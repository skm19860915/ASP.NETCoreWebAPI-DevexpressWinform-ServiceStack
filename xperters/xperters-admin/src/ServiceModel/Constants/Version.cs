namespace Xperters.Admin.ServiceModel.Constants
{
    public static class Version
    {
        public const int Version1 = 1;
        public const int Version2 = 2;
        // version 3 adds BusinessTypeId to LegalEntity autoquery and makes it compulsory
        public const int Version3 = 3;
        // version 4 has migrated the GetAllFundsRequest to use ServiceStack AutoQuery
        public const int Version4 = 4;

        public static readonly int DefaultVersion = Version1;
        public static readonly int CurrentVersion = Version1;
    }
}