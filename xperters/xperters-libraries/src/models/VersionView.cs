namespace xperters.models
{
    public class VersionView
    {
        public string Version { get; set; }
        public string Location { get; set; }
        public string WebsiteAddress { get; set; }
        public string DatabaseConnectionString { get; set; }
        public string EnvironmentVariables { get; set; }
        public bool DotnetRunningInContainer { get; set; }
    }
}
