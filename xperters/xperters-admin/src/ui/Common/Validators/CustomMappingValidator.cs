namespace Xperters.Admin.UI.Common.Validators
{
    public abstract class CustomMappingValidator<T, TCustomFieldMappings> : Validator<T>
        where TCustomFieldMappings : new()
    {
        public CustomMappingValidator()
        {
            CustomFieldMappings = new TCustomFieldMappings();
        }

        public CustomMappingValidator<T, TCustomFieldMappings> WithCustomFieldMappings(TCustomFieldMappings customFieldMappings)
        {
            if (customFieldMappings == null)
                CustomFieldMappings = new TCustomFieldMappings();
            else
                CustomFieldMappings = customFieldMappings;
            return this;
        }

        protected TCustomFieldMappings CustomFieldMappings { get; set; }
    }
}
