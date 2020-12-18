using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xperters.Admin.UI.Common.Extensions;

namespace Xperters.Admin.UI
{
    public class PropertyCopier
    {
#if DEBUG
        // This string builder is used to write out the recursive process implemented here, which you can then grab to stick in your callsite's logger. 
        // We can't use ILoggerdirectly since this project doesn't know about Xperters.Core, and can't reference it because it's framework.
        public StringBuilder StringBuilder { get; } = new StringBuilder();
#endif

        /// <summary>
        /// Copies a specific property from one object to another object. Both objects must be of the same type.
        /// Nested properties can be copied by specifying a "." delimited property path.
        /// E.g. "RootObjectPropertyName.NestedPropertyName1.DoubleNestedPropertyName", etc.
        /// Collections are not supported.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyPath"></param>
        /// <param name="target"></param>
        public void CopyToExactMatchTargetProperty(
            object source,
            string propertyPath,
            object target)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (propertyPath == null)
                throw new ArgumentNullException(nameof(propertyPath));
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            if (string.IsNullOrWhiteSpace(propertyPath))
                throw new ArgumentException("Argument may not be empty or whitespace", nameof(propertyPath));
            if (propertyPath.Contains(" "))
                throw new ArgumentException("Argument may not contain spaces", nameof(propertyPath));

            string[] pathElements = propertyPath.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (pathElements.Length > 1)
            {
                string firstPathElement = pathElements[0];
                string[] remainingElements = pathElements.Skip(1).Take(pathElements.Length - 1).ToArray();
                string remainingPath = string.Join(".", remainingElements);

                var sourceProperty = source.GetType().GetProperties().SingleOrDefault(p => p.Name == firstPathElement);
                if (sourceProperty == null)
                    throw new ArgumentException($"Property '{firstPathElement}' not found on the {nameof(source)}", nameof(propertyPath));
                object nestedSource = sourceProperty.GetValue(source);

                var targetProperty = target.GetType().GetProperties().SingleOrDefault(p => p.Name == firstPathElement);
                if (targetProperty == null)
                    throw new ArgumentException($"Property '{firstPathElement}' not found on the {nameof(target)}", nameof(propertyPath));
                object nestedTarget = targetProperty.GetValue(target);

                CopyToExactMatchTargetProperty(nestedSource, remainingPath, nestedTarget);
            }
            else
            {
                var sourceProperty = source.GetType().GetProperties().SingleOrDefault(p => p.Name == propertyPath);
                if (sourceProperty == null)
                    throw new ArgumentException($"Property path '{propertyPath}' not found on the {nameof(source)}", nameof(propertyPath));
                var targetProperty = target.GetType().GetProperties().SingleOrDefault(p => p.Name == propertyPath);
                if (targetProperty == null)
                    throw new ArgumentException($"Property path '{propertyPath}' not found on the {nameof(target)}", nameof(propertyPath));

                if (sourceProperty.PropertyType == targetProperty.PropertyType)
                    targetProperty.SetValue(target, sourceProperty.GetValue(source));
                else
                    throw new InvalidOperationException($"The property '{propertyPath}' is of type '{sourceProperty.PropertyType.FullName}' on the {nameof(source)}, but of type '{targetProperty.PropertyType.FullName}' on the {nameof(target)}.");
            }
        }

        /// <summary>
        /// Copies a value to a property on an object.
        /// </summary>
        /// <param name="propertyPath"></param>
        /// <param name="target"></param>
        public void CopyValueToTargetProperty(
            object value,
            string propertyPath,
            object target)
        {
            if (propertyPath == null)
                throw new ArgumentNullException(nameof(propertyPath));
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            if (string.IsNullOrWhiteSpace(propertyPath))
                throw new ArgumentException("Argument may not be empty or whitespace", nameof(propertyPath));
            if (propertyPath.Contains(" "))
                throw new ArgumentException("Argument may not contain spaces", nameof(propertyPath));

            string[] pathElements = propertyPath.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (pathElements.Length > 1)
            {
                string firstPathElement = pathElements[0];
                string[] remainingElements = pathElements.Skip(1).Take(pathElements.Length - 1).ToArray();
                string remainingPath = string.Join(".", remainingElements);

                var targetProperty = target.GetType().GetProperties().SingleOrDefault(p => p.Name == firstPathElement);
                if (targetProperty == null)
                    throw new ArgumentException($"Property '{firstPathElement}' not found on the {nameof(target)}", nameof(propertyPath));
                object nestedTarget = targetProperty.GetValue(target);

                CopyToExactMatchTargetProperty(value, remainingPath, nestedTarget);
            }
            else
            {

                var targetProperty = target.GetType().GetProperties().SingleOrDefault(p => p.Name == propertyPath);
                if (targetProperty == null)
                    throw new ArgumentException($"Property path '{propertyPath}' not found on the {nameof(target)}", nameof(propertyPath));

                if (value.GetType() == targetProperty.PropertyType)
                    targetProperty.SetValue(target, value);
                else
                    throw new InvalidOperationException($"The value is of type '{value.GetType().FullName}', but of type '{targetProperty.PropertyType.FullName}' on the {nameof(target)}.");
            }
        }

        /// <summary>
        /// Copies a specific property from one source object to one ore more target object trees.
        /// The source property to be copied can be a nested property by specifying a "." delimited property path.
        /// E.g. "RootObjectPropertyName.NestedPropertyName1.DoubleNestedPropertyName", etc.
        /// In the above example, "DoubleNestedPropertyName" is the leaf property name.
        /// The property value will be copied to all matching properties on any of the target objects or nested target objects.
        /// A target leaf property is considered a match only when both the following conditions are met:
        /// 1) The target leaf property name is equal to the source leaf property name
        /// 2) The type of the object that owns the target leaf property is equal to the type of the object that owns the source leaf property.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sourcePropertyPath"></param>
        /// <param name="targets"></param>
        public void CopyToAllMatchingTargetProperties(
            object source,
            string sourcePropertyPath,
            params object[] targets)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (sourcePropertyPath == null)
                throw new ArgumentNullException(nameof(sourcePropertyPath));
            if (targets == null)
                throw new ArgumentNullException(nameof(targets));
            if (string.IsNullOrWhiteSpace(sourcePropertyPath))
                throw new ArgumentException("Argument may not be empty or whitespace", nameof(sourcePropertyPath));
            if (sourcePropertyPath.Contains(" "))
                throw new ArgumentException("Argument may not contain spaces", nameof(sourcePropertyPath));

            var valueTuple = GetPropertyValueAndMetadata(source, sourcePropertyPath);

            CopyToAllMatchingTargetProperties(
                valueTuple.owningType,
                valueTuple.leafPropertyName,
                new ValueWrapper(valueTuple.leafPropertyType, valueTuple.leafPropertyValue),
                null,
                0,
                targets);
        }

        public void CopyToAllMatchingTargetProperties(
            Type targetObjectType,
            string targetLeafPropertyName,
            ValueWrapper valueToSet,
            List<object> alreadyProcessedReferences,
            int recursiveLevel,
            params object[] targets)
        {
            if (targetObjectType == null)
                throw new ArgumentNullException(nameof(targetObjectType));
            if (targetLeafPropertyName == null)
                throw new ArgumentNullException(nameof(targetLeafPropertyName));
            if (targets == null)
                throw new ArgumentNullException(nameof(targets));
            if (string.IsNullOrWhiteSpace(targetLeafPropertyName))
                throw new ArgumentException("Argument may not be empty or whitespace", nameof(targetLeafPropertyName));
            if (targetLeafPropertyName.Contains(" "))
                throw new ArgumentException("Argument may not contain spaces", nameof(targetLeafPropertyName));

            if (alreadyProcessedReferences == null)
                alreadyProcessedReferences = new List<object>();

            targets = targets.Where(t => t != null).ToArray();
            targets = FlattenCollections(targets);

            foreach (var target in targets)
            {
                if (alreadyProcessedReferences.Any(o => ReferenceEquals(o, target)))
                    continue;
                alreadyProcessedReferences.Add(target);

#if DEBUG
                StringBuilder.Append(WithPad(string.Empty, recursiveLevel, '>'));
#endif
                SetPropertyIfMatchFound(targetObjectType, targetLeafPropertyName, valueToSet, target);
#if DEBUG
                StringBuilder.AppendLine(string.Empty);
#endif

                object[] nestedObjects = GetReferenceObjectsOffAllProperties(target);
                nestedObjects = FlattenCollections(nestedObjects);

#if DEBUG
                if (nestedObjects.Length > 0)
                {
                    StringBuilder.AppendLine(WithPad("Checking nested properties:", recursiveLevel, '>'));
                    nestedObjects.ForEach(x => StringBuilder.AppendLine(WithPad($" - {x?.GetType()?.Name ?? "null"}", recursiveLevel, '>')));
                }
                else
                {
                    StringBuilder.AppendLine(WithPad("No nested properties", recursiveLevel, '>'));
                }
#endif

                CopyToAllMatchingTargetProperties(
                    targetObjectType,
                    targetLeafPropertyName,
                    valueToSet,
                    alreadyProcessedReferences,
                    recursiveLevel + 1,
                    nestedObjects);
            }
        }

#if DEBUG
        private static string WithPad(string value, int count, char pad)
        {
            return value.PadLeft(count + value.Length, pad);
        }
#endif

        public static object[] FlattenCollections(object[] objects)
        {
            if (objects == null)
                throw new ArgumentNullException(nameof(objects));

            var flatList = new List<object>();
            objects.ForEach(o =>
            {
                if (o is System.Collections.IEnumerable collection)
                {
                    foreach (object collectionItem in collection)
                        flatList.Add(collectionItem);
                }
                else
                    flatList.Add(o);
            });

            return flatList.ToArray();
        }

        public static object[] GetReferenceObjectsOffAllProperties(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            return obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(p => !p.GetAccessors().Any(a => a.IsPrivate))
                .Where(p => !IsValueType(p))
                // Exclude properties that are of type Action<> or Func<>
                .Where(p => p.PropertyType.BaseType != typeof(MulticastDelegate))
                // Exclude indexed properties
                .Where(p => p.GetIndexParameters().Length == 0)
                // Exclude decorated properties
                .Where(p => p.GetCustomAttribute<PropertyCopierIgnoreAttribute>() == null)
                .Where(p => p.CanRead)
                .Select(p => p.GetValue(obj))
                .ToArray();
        }

        private static bool IsValueType(PropertyInfo p)
        {
            return p.PropertyType.IsValueType
                   || p.PropertyType == typeof(string);
        }

        /// <summary>
        /// Starts at the leaf of the property path and walks the path backward looking for an instance of T.
        /// If a T is found, it is returned.
        /// If no T is found by the time we get to the root of the path, an exception is thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The root object to walk on.</param>
        /// <param name="propertyPath">The path to walk backward looking for an instance of T.</param>
        /// <returns>T, if it is found.</returns>
        public T GetInstanceFromPath<T>(
            object obj,
            string propertyPath)
        {
            var valueTuple = GetPropertyValueAndMetadata(obj, propertyPath);
            if (valueTuple.leafPropertyType == typeof(T))
                return (T)valueTuple.leafPropertyValue;
            if (valueTuple.owningType == typeof(T))
                return (T)valueTuple.owningObjectInstance;

            if (!propertyPath.Contains("."))
                throw new Exception($"We have walked backward up the property path and have not found an instance of {typeof(T).FullName} by the time we got to the root object of type {obj.GetType().FullName}.");

            string parentPropertyPath = GetParentPropertyPath(propertyPath);
            return GetInstanceFromPath<T>(obj, parentPropertyPath);
        }

        private void SetPropertyIfMatchFound(Type targetObjectType, string propertyName, ValueWrapper valueWrapper, object target)
        {
            if (target.GetType() == targetObjectType)
            {
#if DEBUG
                StringBuilder.Append($"<{targetObjectType.Name}> is <{target}> -> ");
#endif
                var property = target.GetType().GetProperty(propertyName, valueWrapper.ValueType);

                // If the type of the value is a ValueType, and we did not find a property match with the value's value type,
                // also look for a property that has a nullable version of the value type.
                // Eg. if the value is of type 'decimal' and we did not find a matching property with that name and type,
                // also look for a property with the type of 'decimal?'.
                // I.e. you can set a 'decimal' into a 'decimal?'.
                if (property == null && valueWrapper.GetType().IsValueType)
                {
                    Type nullableValueType = typeof(Nullable<>).MakeGenericType(valueWrapper.ValueType);
                    property = target.GetType().GetProperty(propertyName, nullableValueType);
                }

                if (property != null)
                {
                    if (property.GetCustomAttribute<PropertyCopierIgnoreAttribute>() != null)
                    {
                        throw new InvalidOperationException($"Property '{propertyName}' is set to ignore, but is specified as the target");
                    }

                    // Lief property found on this target matching our property name and value type.
                    if (property.CanWrite)
                    {
                        property.SetValue(target, valueWrapper.Value);
#if DEBUG
                        StringBuilder.Append($"Setting value to '{valueWrapper.Value}'");
#endif
                    }
                }
            }
        }

        public (Type owningType, object owningObjectInstance, string leafPropertyName, Type leafPropertyType, bool leafPropertyCanWrite, object leafPropertyValue) GetPropertyValueAndMetadata(
            object obj,
            string propertyPath)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            if (propertyPath == null)
                throw new ArgumentNullException(nameof(propertyPath));
            if (string.IsNullOrWhiteSpace(propertyPath))
                throw new ArgumentException("Argument may not be empty or whitespace", nameof(propertyPath));
            if (propertyPath.Contains(" "))
                throw new ArgumentException("Argument may not contain spaces", nameof(propertyPath));

            string[] pathElements = propertyPath.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (pathElements.Length > 1)
            {
                string firstPathElement = pathElements[0];
                string[] remainingElements = pathElements.Skip(1).Take(pathElements.Length - 1).ToArray();
                string remainingPath = string.Join(".", remainingElements);

                var intermediateProperty = obj.GetType().GetProperty(firstPathElement);
                if (intermediateProperty == null)
                    throw new ArgumentException($"Property '{firstPathElement}' not found on the {nameof(obj)}", nameof(propertyPath));
                if (!intermediateProperty.CanRead)
                    throw new ArgumentException($"Property '{firstPathElement}' on the {nameof(obj)} cannot be read from.", nameof(propertyPath));
                object nestedObj = intermediateProperty.GetValue(obj);

                return GetPropertyValueAndMetadata(nestedObj, remainingPath);
            }
            else // We are on the last part of the path - the leaf property.
            {
                var leafProperty = obj.GetType().GetProperty(propertyPath);
                if (leafProperty == null)
                    throw new ArgumentException($"Property path '{propertyPath}' not found on the {nameof(obj)}", nameof(propertyPath));
                if (!leafProperty.CanRead)
                    throw new ArgumentException($"Property path '{propertyPath}' cannot be read from; {nameof(obj)}", nameof(propertyPath));

                return (obj.GetType(), obj, leafProperty.Name, leafProperty.PropertyType, leafProperty.CanWrite, leafProperty.GetValue(obj));
            }
        }


        /// <summary>
        /// If any value in the property path is null it will return null for both the type and property value
        /// </summary>
        /// <returns></returns>
        public (Type leafPropertyType, object leafPropertyValue) GetPropertyValue(
            object obj,
            string propertyPath)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            if (propertyPath == null)
                throw new ArgumentNullException(nameof(propertyPath));
            if (string.IsNullOrWhiteSpace(propertyPath))
                throw new ArgumentException("Argument may not be empty or whitespace", nameof(propertyPath));
            if (propertyPath.Contains(" "))
                throw new ArgumentException("Argument may not contain spaces", nameof(propertyPath));

            string[] pathElements = propertyPath.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (pathElements.Length > 1)
            {
                string firstPathElement = pathElements[0];
                string[] remainingElements = pathElements.Skip(1).Take(pathElements.Length - 1).ToArray();
                string remainingPath = string.Join(".", remainingElements);

                var intermediateProperty = obj.GetType().GetProperty(firstPathElement);
                if (intermediateProperty == null)
                    throw new ArgumentException($"Property '{firstPathElement}' not found on the {nameof(obj)} of type {obj?.GetType().FullName}", nameof(propertyPath));
                if (!intermediateProperty.CanRead)
                    throw new ArgumentException($"Property '{firstPathElement}' on the {nameof(obj)} of type {obj?.GetType().FullName} cannot be read from.", nameof(propertyPath));
                object nestedObj = intermediateProperty.GetValue(obj);

                if (nestedObj == null)
                    return (null, null);
                return GetPropertyValue(nestedObj, remainingPath);
            }
            else // We are on the last part of the path - the leaf property.
            {
                var leafProperty = obj.GetType().GetProperty(propertyPath);
                if (leafProperty == null)
                    throw new ArgumentException($"Lief property path '{propertyPath}' not found on the {nameof(obj)} of type {obj?.GetType().FullName}", nameof(propertyPath));
                if (!leafProperty.CanRead)
                    throw new ArgumentException($"Lief property path '{propertyPath}' cannot be read from; {nameof(obj)} of type {obj?.GetType().FullName}", nameof(propertyPath));

                return (leafProperty.PropertyType, leafProperty.GetValue(obj));
            }
        }

        public class ValueWrapper
        {
            public ValueWrapper(
                Type valueType,
                object value)
            {
                ValueType = valueType ?? throw new ArgumentNullException(nameof(valueType));
                // No null check as value may be null.
                Value = value;
            }

            public Type ValueType { get; set; }
            public object Value { get; set; }
        }

        public string GetParentPropertyPath(string propertyPath)
        {
            if (propertyPath == null)
                throw new ArgumentNullException(nameof(propertyPath));
            if (string.IsNullOrWhiteSpace(propertyPath))
                throw new ArgumentException("May not be empty or whitespace.", nameof(propertyPath));

            var parts = propertyPath.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (parts.Count() == 1)
                throw new Exception($"The property path '{propertyPath}' does not have a parent element.");

            parts = parts.Take(parts.Count() - 1).ToArray();
            string parentPath = string.Join(".", parts);
            return parentPath;
        }

        public string GetLeafPropertyName(
            string propertyPath)
        {
            if (string.IsNullOrWhiteSpace(propertyPath))
            {
                throw new ArgumentException("message", nameof(propertyPath));
            }

            string[] parts = propertyPath.Split('.');
            string propertyName = parts[parts.Length - 1];
            return propertyName;
        }

        public (Type parentPropertyType, object parentPropertyValue) GetParentPropertyValue(
            object obj,
            string propertyPath)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (string.IsNullOrWhiteSpace(propertyPath))
            {
                throw new ArgumentException("message", nameof(propertyPath));
            }

            var parentPath = GetParentPropertyPath(propertyPath);
            var val = GetPropertyValue(obj, parentPath);
            return (val.leafPropertyType, val.leafPropertyValue);
        }
    }
}