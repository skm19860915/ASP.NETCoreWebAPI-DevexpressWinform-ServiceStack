using ServiceStack.DataAnnotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xperters.Admin.ServiceModel.ChangeTracking;

namespace Xperters.Admin.ServiceModel
{
	public abstract class Bindable : IBindableNotifyPropertyChanged
	{
		public Bindable()
		{
			IsConstructionPhaseComplete = true;
		}

		[Ignore]
		public bool ShouldPersist { get; set; } = true;
		private bool IsConstructionPhaseComplete { get; set; } = false;

		#region Property Dictionaries
		// We use dictionaries of different types to store the different property types instead of just in a Dictionary<string, object> to prevent boxing and unboxing. 
		// This is because boxing and unboxing has a significant performance hit, and these getters and setters are called millions of times during a simple op such as running a report, etc.
		protected Dictionary<string, string> _propertiesString = new Dictionary<string, string>();
		protected Dictionary<string, bool> _propertiesBool = new Dictionary<string, bool>();
		protected Dictionary<string, bool?> _propertiesNullableBool = new Dictionary<string, bool?>();
		protected Dictionary<string, int> _propertiesInt = new Dictionary<string, int>();
		protected Dictionary<string, int?> _propertiesNullableInt = new Dictionary<string, int?>();
		protected Dictionary<string, long> _propertiesLong = new Dictionary<string, long>();
		protected Dictionary<string, long?> _propertiesNullableLong = new Dictionary<string, long?>();
		protected Dictionary<string, decimal> _propertiesDecimal = new Dictionary<string, decimal>();
		protected Dictionary<string, decimal?> _propertiesNullableDecimal = new Dictionary<string, decimal?>();
		protected Dictionary<string, DateTime> _propertiesDateTime = new Dictionary<string, DateTime>();
		protected Dictionary<string, DateTime?> _propertiesNullableDateTime = new Dictionary<string, DateTime?>();
		// Finally, a fallback to object to catch edge cases	
		protected Dictionary<string, object> _propertiesObject = new Dictionary<string, object>();

		#endregion Property Dictionaries

		private HashSet<string> DirtyPropertyNames { get; } = new HashSet<string>();
		private bool DirtyPropertyNamesTrackingEnabled { get; set; } = false;

		#region Typed Getters and Setters

		protected string GetString([CallerMemberName] string name = null)
		{
			if (_propertiesString.TryGetValue(name, out string value))
				return value;
			return default(string);
		}

		protected void SetString(string value, [CallerMemberName] string name = null)
		{
			var oldValue = GetString(name);
			if (value == oldValue)
				return;

			if (IsConstructionPhaseComplete)
				ShouldPersist = true;

			_propertiesString[name] = value;
			OnPropertyChanged(oldValue, value, name);
		}

		protected bool GetBool([CallerMemberName] string name = null)
		{
			if (_propertiesBool.TryGetValue(name, out bool value))
				return value;
			return default(bool);
		}

		protected void SetBool(bool value, [CallerMemberName] string name = null)
		{
			var oldValue = GetBool(name);
			if (value == oldValue)
				return;

			if (IsConstructionPhaseComplete)
				ShouldPersist = true;

			_propertiesBool[name] = value;
			OnPropertyChanged(oldValue, value, name);
		}

		protected bool? GetNullableBool([CallerMemberName] string name = null)
		{
			if (_propertiesNullableBool.TryGetValue(name, out bool? value))
				return value;
			return default(bool?);
		}

		protected void SetNullableBool(bool? value, [CallerMemberName] string name = null)
		{
			var oldValue = GetNullableBool(name);
			if (value == oldValue)
				return;

			if (IsConstructionPhaseComplete)
				ShouldPersist = true;

			_propertiesNullableBool[name] = value;
			OnPropertyChanged(oldValue, value, name);
		}

		protected int GetInt([CallerMemberName] string name = null)
		{
			if (_propertiesInt.TryGetValue(name, out int value))
				return value;
			return default(int);
		}

		protected void SetInt(int value, [CallerMemberName] string name = null)
		{
			var oldValue = GetInt(name);
			if (value == oldValue)
				return;

			if (IsConstructionPhaseComplete)
				ShouldPersist = true;

			_propertiesInt[name] = value;
			OnPropertyChanged(oldValue, value, name);
		}

		protected int? GetNullableInt([CallerMemberName] string name = null)
		{
			if (_propertiesNullableInt.TryGetValue(name, out int? value))
				return value;
			return default(int?);
		}

		protected void SetNullableInt(int? value, [CallerMemberName] string name = null)
		{
			var oldValue = GetNullableInt(name);
			if (value == oldValue)
				return;

			if (IsConstructionPhaseComplete)
				ShouldPersist = true;

			_propertiesNullableInt[name] = value;
			OnPropertyChanged(oldValue, value, name);
		}

		protected long GetLong([CallerMemberName] string name = null)
		{
			if (_propertiesLong.TryGetValue(name, out long value))
				return value;
			return default(long);
		}

		protected void SetLong(long value, [CallerMemberName] string name = null)
		{
			var oldValue = GetLong(name);
			if (value == oldValue)
				return;

			if (IsConstructionPhaseComplete)
				ShouldPersist = true;

			_propertiesLong[name] = value;
			OnPropertyChanged(oldValue, value, name);
		}

		protected long? GetNullableLong([CallerMemberName] string name = null)
		{
			if (_propertiesNullableLong.TryGetValue(name, out long? value))
				return value;
			return default(long?);
		}

		protected void SetNullableLong(long? value, [CallerMemberName] string name = null)
		{
			var oldValue = GetNullableLong(name);
			if (value == oldValue)
				return;

			if (IsConstructionPhaseComplete)
				ShouldPersist = true;

			_propertiesNullableLong[name] = value;
			OnPropertyChanged(oldValue, value, name);
		}

		protected decimal GetDecimal([CallerMemberName] string name = null)
		{
			if (_propertiesDecimal.TryGetValue(name, out decimal value))
				return value;
			return default(decimal);
		}

		protected void SetDecimal(decimal value, [CallerMemberName] string name = null)
		{
			var oldValue = GetDecimal(name);
			if (value == oldValue)
				return;

			if (IsConstructionPhaseComplete)
				ShouldPersist = true;

			_propertiesDecimal[name] = value;
			OnPropertyChanged(oldValue, value, name);
		}

		protected decimal? GetNullableDecimal([CallerMemberName] string name = null)
		{
			if (_propertiesNullableDecimal.TryGetValue(name, out decimal? value))
				return value;
			return default(decimal?);
		}

		protected void SetNullableDecimal(decimal? value, [CallerMemberName] string name = null)
		{
			var oldValue = GetNullableDecimal(name);
			if (value == oldValue)
				return;

			if (IsConstructionPhaseComplete)
				ShouldPersist = true;

			_propertiesNullableDecimal[name] = value;
			OnPropertyChanged(oldValue, value, name);
		}


		protected DateTime GetDateTime([CallerMemberName] string name = null)
		{
			if (_propertiesDateTime.TryGetValue(name, out DateTime value))
				return value;
			return default(DateTime);
		}

		protected void SetDateTime(DateTime value, [CallerMemberName] string name = null)
		{
			var oldValue = GetDateTime(name);
			if (value == oldValue)
				return;

			if (IsConstructionPhaseComplete)
				ShouldPersist = true;

			_propertiesDateTime[name] = value;
			OnPropertyChanged(oldValue, value, name);
		}

		protected DateTime? GetNullableDateTime([CallerMemberName] string name = null)
		{
			if (_propertiesNullableDateTime.TryGetValue(name, out DateTime? value))
				return value;
			return default(DateTime?);
		}

		protected void SetNullableDateTime(DateTime? value, [CallerMemberName] string name = null)
		{
			var oldValue = GetNullableDateTime(name);
			if (value == oldValue)
				return;

			if (IsConstructionPhaseComplete)
				ShouldPersist = true;

			_propertiesNullableDateTime[name] = value;
			OnPropertyChanged(oldValue, value, name);
		}



		/// <summary>
		/// Gets the value of a property
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
		protected T GetObject<T>([CallerMemberName] string name = null)
		{
			object value = null;
			if (_propertiesObject.TryGetValue(name, out value))
				return value == null ? default(T) : (T)value;

			return default(T);
		}

		/// <summary>
		/// Sets the value of a property
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <param name="name"></param>
		/// <remarks>Use this overload when implicitly naming the property</remarks>
		protected void SetObject<T>(T value, [CallerMemberName] string name = null)
		{
			var oldValue = GetObject<T>(name);
			if (Equals(value, oldValue))
				return;

			if (IsConstructionPhaseComplete)
			{
				var property = GetType().GetProperty(name);
				var propertyType = property.PropertyType;
				//If its a reference property it does not count as a change on this object
				var isReferenceProperty = property.GetCustomAttributes(typeof(ReferenceAttribute), false).Length > 0;
				if (!isReferenceProperty)
					ShouldPersist = true;
			}

			_propertiesObject[name] = value;
			OnPropertyChanged(oldValue, value, name);
		}

		#endregion Typed Getters and Setters

		public event BindablePropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(
			object oldValue,
			object newValue,
			[CallerMemberName]
			string propertyName = null,
			BindablePropertyChangedReason reason = BindablePropertyChangedReason.PropertySet)
		{
			if (DirtyPropertyNamesTrackingEnabled
				&& IsConstructionPhaseComplete
				&& !string.IsNullOrWhiteSpace(propertyName)
				&& reason == BindablePropertyChangedReason.PropertySet)
			{
				MarkPropertyAsDirty(propertyName);
			}

			BindablePropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new BindablePropertyChangedEventArgs(oldValue, newValue, propertyName, reason));

            var property = GetType().GetProperty(propertyName);
			if (property.PropertyType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(property.PropertyType.GetGenericTypeDefinition()))
			{
				if (typeof(Bindable).IsAssignableFrom(property.PropertyType.GetGenericArguments()[0]))
				{
					var collection = (IEnumerable)property.GetValue(this, null);
					if (collection == null)
						return;
					foreach (var item in collection)
					{
                        ((Bindable) item)?.NotifyAllProperties(BindablePropertyChangedReason.PropertyOwnerCollectionReplaced);
					}
				}
			}
		}

		public void NotifyAllPropertiesOnAdd()
		{
			NotifyAllProperties(BindablePropertyChangedReason.PropertyOwnerAddedToCollection);
		}

		public void NotifyAllPropertiesOnRemove()
		{
			NotifyAllProperties(BindablePropertyChangedReason.PropertyOwnerRemovedFromCollection);
		}

		public void NotifyPropertyChanged(
			string propertyName,
			BindablePropertyChangedReason reason)
		{
			BindablePropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new BindablePropertyChangedEventArgs(null, null, propertyName, reason));
			}
		}

		private void NotifyAllProperties(BindablePropertyChangedReason reason)
		{
			foreach (var property in GetType().GetProperties())
			{
				OnPropertyChanged(null, null, property.Name, reason);
			}
		}

		public void ResetChangeTrackingAndEventHandler(BindablePropertyChangedEventHandler handler)
		{
			PropertyChanged = null;
			PropertyChanged += handler;
			ShouldPersist = IsNew();
			DirtyPropertyNames.Clear();
			DirtyPropertyNamesTrackingEnabled = true;
		}

		protected abstract bool IsNew();

		private void MarkPropertyAsDirty(
			string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
				throw new ArgumentException("message", nameof(propertyName));

			DirtyPropertyNames.Add(propertyName);
		}

		public bool IsPropertyDirty(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
				throw new ArgumentException("message", nameof(propertyName));

			return DirtyPropertyNames.Contains(propertyName);
		}

	}

    public interface IBindableNotifyPropertyChanged
    {
    }
}
