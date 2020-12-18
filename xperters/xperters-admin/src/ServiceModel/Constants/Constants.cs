namespace Xperters.Admin.ServiceModel.Constants
{
    public static class Constants
    {
        public const string Milestones = "Milestones";
        public const string Jobs = "Jobs";
        public const string VersionTag = "Version";
        public const string Identity = "Identity";
        public const string Withdrawals = "Withdrawals";
        public const string Incoming = "Incoming";
        public const string Users = "Users";

		public static readonly string DefaultChannel = "Default";

        public const string DbIgnitionSchema = "ign";
        public const long Transient = -1;

        public static class Version
        {
            public const int Version1 = 1;
            public const int Version2 = 2;
            public const int Version3 = 3;
            public const int Version4 = 4;

            public static readonly int DefaultVersion = Version1;
            public static readonly int CurrentVersion = Version1;
        }
        
		public static class Db
		{
			public const long TransientId = -1;

			public static class Schemas
			{
				public const string Fas = "fas";
			}
		}

		public static class AzureAd
		{
			public static readonly string AzureAdConfigurationKey = "AzureAd";
		}
		
    }
}