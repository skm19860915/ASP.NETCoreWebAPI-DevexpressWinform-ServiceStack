using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.XtraEditors.DXErrorProvider;
using FluentValidation;
using Xperters.Admin.ServiceModel.Extensions;

namespace Xperters.Admin.UI.Common
{
    public abstract class ViewModel : IDXDataErrorInfo
    {
        protected Dictionary<string, ValidationInfo> PropertyErrors { get; set; } = new Dictionary<string, ValidationInfo>();

        private Dictionary<string, (ServiceModel.Bindable bindable, string bindablePropertyName)> ViewModelPropertyToOwningBindableMap { get; set; } = new Dictionary<string, (ServiceModel.Bindable bindable, string bindablePropertyName)>();

        public IEnumerable<(string field, string title, string error, Severity severity)> Errors
            => PropertyErrors
                .OrderBy(o => o.Value.Severity)
                .ThenBy(o => o.Key)
                .ThenBy(o => o.Value.Message)
                .Select(o => (o.Key, o.Value.Title, o.Value.Message, o.Value.Severity));

        public IEnumerable<(string field, string title, string error, Severity severity)> ConsolidatedErrors
            => Errors
                .Select(o => new
                {
                    Field = o.field.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).LastOrDefault() ?? $"Provide to support: Error IGN-8329 - Unexpected field value: '{o.field}'",
                    Error = o.error,
                    Severity = o.severity,
                    Title = o.title
                })
                .GroupBy(o => o.Field)
                .Select(g => g.OrderBy(o => o.Severity)
                    .ThenBy(o => o.Title.Equals(o.Field, StringComparison.InvariantCultureIgnoreCase)).First()
                )
                .Select(o => (o.Field, o.Title, o.Error, o.Severity));

        public bool HasSeverityErrerFailures
            => Errors.Any(o => o.severity == Severity.Error);

        public bool HasSeverityWarningFailures
            => Errors.Any(o => o.severity == Severity.Warning);

        public string Error
        {
            get
            {
                if (PropertyErrors.Count == 0)
                    return null;

                var errorMessages = PropertyErrors
                    .OrderBy(o => o.Value.Severity)
                    .ThenBy(o => o.Value.Message)
                    .Select(o => o.Value.Message).Distinct();

                return string.Join(",", errorMessages);
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (PropertyErrors.ContainsKey(columnName))
                    return PropertyErrors[columnName].Message;
                else
                    return null;
            }
        }

        public void GetPropertyError(string propertyName, ErrorInfo info)
        {
            if (PropertyErrors.ContainsKey(propertyName))
            {
                var error = PropertyErrors[propertyName];
                info.ErrorText = error.Message;
                switch (error.Severity)
                {
                    case Severity.Info:
                        info.ErrorType = ErrorType.Information;
                        break;

                    case Severity.Warning:
                        info.ErrorType = ErrorType.Warning;
                        break;

                    case Severity.Error:
                        info.ErrorType = ErrorType.Default;
                        break;

                    default:
                        info.ErrorType = ErrorType.Critical;
                        break;
                }
            }
            else
            {
                info.ErrorType = ErrorType.None;
            }
        }

        public void GetError(ErrorInfo info)
        {
        }

        protected class ValidationInfo
        {
            internal string Message { get; set; }
            internal Severity Severity { get; set; }
            internal string Title { get; set; }
        }

        protected void MapViewModelPropertyToOwningBindable<TViewModel, TViewModelProperty, TBindable, TBindableProperty>(
            Expression<Func<TViewModel, TViewModelProperty>> viewModelPropertyNameExpression,
            ServiceModel.Bindable owningBindable,
            Expression<Func<TBindable, TBindableProperty>> bindablePropertyNameExpression)
            where TBindable : ServiceModel.Bindable
            where TViewModel : ViewModel<TViewModel>
        {
            if (viewModelPropertyNameExpression == null)
                throw new ArgumentNullException(nameof(viewModelPropertyNameExpression));
            if (owningBindable is null)
                throw new ArgumentNullException(nameof(owningBindable));
            if (bindablePropertyNameExpression is null)
                throw new ArgumentNullException(nameof(bindablePropertyNameExpression));

            var vmPropertyName = viewModelPropertyNameExpression.GetPropertyPath();
            var bindablePropertyName = bindablePropertyNameExpression.GetPropertyPath();

            if (ViewModelPropertyToOwningBindableMap.ContainsKey(vmPropertyName))
            {
                var map = ViewModelPropertyToOwningBindableMap[vmPropertyName];
                if (map.bindablePropertyName != bindablePropertyName)
                {
                    throw new Exception($"The ViewModel property {GetType().FullName}.{vmPropertyName} is already mapped to {owningBindable.GetType().FullName}.{map.bindablePropertyName}, and cannot be mapped a second time to {owningBindable.GetType().FullName}.{bindablePropertyName}. Make sure you map it only once.");
                }
            }
            ViewModelPropertyToOwningBindableMap[vmPropertyName] = (owningBindable, bindablePropertyName);
        }

        /// <summary>
        /// Returns the instance of the Bindable that was mapped as the owner of the ViewModel property name.
        /// Returns a tuple with a null Bindable instance if no mapping was found.
        /// </summary>
        /// <param name="viewModelPropertyName"></param>
        /// <returns></returns>
        public (ServiceModel.Bindable bindable, string bindablePropertyName) GetMappedBindableFromViewModelPropertyName(
            string viewModelPropertyName)
        {
            if (string.IsNullOrWhiteSpace(viewModelPropertyName))
            {
                throw new ArgumentException("message", nameof(viewModelPropertyName));
            }

            if (ViewModelPropertyToOwningBindableMap.ContainsKey(viewModelPropertyName))
            {
                return ViewModelPropertyToOwningBindableMap[viewModelPropertyName];
            }
            else
            {
                return (bindable: null, bindablePropertyName: string.Empty);
            }
        }

    }
}