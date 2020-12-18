namespace Xperters.Core.Configuration
{
    public interface IConnectionStringSettings
    {
        string Name { get; set; }
        string ConnectionString { get; set; }
        string ProviderName { get; set; }
    }
}