namespace Xperters.Admin.ServiceModel.Validations
{
    public sealed class NullPropertyTitleProvider : IPropertyTitleProvider
    {
        public string GetTitle(string propertyPath)
        {
            return null;
        }
    }
}