using AutoMapper;
namespace xperters.mockdata.Mapping
{
    public static class AutoMapperConfig
    {
        private static readonly object ThisLock = new object();
        private static bool _initialized;
        private static IMapper _initializedMapper;

        public static IMapper InitializedMapper
        {
            get
            {
                if (!_initialized)
                {
                    Initialize();
                }

                return _initializedMapper;
            }
            private set => _initializedMapper = value;
        }

        // Centralize automapper initialize
        public static void Initialize()

        {
            // This will ensure one thread can access to this static initialize call
            // and ensure the mapper is reseted before initialized
            lock (ThisLock)
            {
                if (!_initialized)
                {
                    Mapper.Reset();
                    Mapper.Initialize(cfg =>
                    {
                        var profile = new MappingProfileFakes();
                        
                        cfg.AddProfile(profile);
                    });

                    Mapper.Configuration.AssertConfigurationIsValid();
                    InitializedMapper = Mapper.Configuration.CreateMapper();

                    Mapper.AssertConfigurationIsValid();

                    _initialized = true;
                }
            }
        }
    }
}
