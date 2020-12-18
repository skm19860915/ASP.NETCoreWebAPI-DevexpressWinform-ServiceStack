using System;
using Xperters.Admin.ServiceModel;

namespace Xperters.Admin.UI.Common.Extensions
{
	public static class ViewModelExtensions
	{
		public static (Bindable bindable, string bindablePropertyName) GetBindableAndPropertyName(
			this ViewModel viewModel,
			string propertyPath)
		{
			if (viewModel is null)
				throw new ArgumentNullException(nameof(viewModel));
			if (string.IsNullOrWhiteSpace(propertyPath))
				throw new ArgumentException($"{nameof(propertyPath)} may not be null or white space. ViewModel type is {viewModel.GetType().FullName}", nameof(propertyPath));

			PropertyCopier propertyCopier = new PropertyCopier();
			string propertyName = propertyCopier.GetLeafPropertyName(propertyPath);

			if (propertyName == propertyPath)
			{
				// the path only contains a leaf property.
				return viewModel.GetMappedBindableFromViewModelPropertyName(propertyName);
			}

			var parentProperty = propertyCopier.GetParentPropertyValue(viewModel, propertyPath);
			if (parentProperty.parentPropertyValue is Bindable bindable)
			{
				return (bindable: bindable, bindablePropertyName: propertyName);
			}
			else if (parentProperty.parentPropertyValue is ViewModel vm)
			{
				return vm.GetMappedBindableFromViewModelPropertyName(propertyName);
			}
			else
			{
				throw new NotSupportedException($"Expected a type deriving either from {nameof(Bindable)} or from {nameof(ViewModel)}, but found an insttance of {parentProperty.parentPropertyValue?.GetType().FullName ?? $"(null reference)"}; Relating to the property path: '{propertyPath}' on ViewModel of type {viewModel.GetType().FullName}");
			}
		}

		public static bool IsBindablePropertyDirty(
			this ViewModel viewModel,
			string propertyPath)
		{
			if (viewModel is null)
				throw new ArgumentNullException(nameof(viewModel));
			if (string.IsNullOrWhiteSpace(propertyPath))
				throw new ArgumentException("message", nameof(propertyPath));

			(Bindable bindable, string bindablePropertyName) bindableInfo = viewModel.GetBindableAndPropertyName(propertyPath);
			if (bindableInfo.bindable != null)
			{
				return bindableInfo.bindable.IsPropertyDirty(bindableInfo.bindablePropertyName);
			}
			return false;
		}


	}
}
