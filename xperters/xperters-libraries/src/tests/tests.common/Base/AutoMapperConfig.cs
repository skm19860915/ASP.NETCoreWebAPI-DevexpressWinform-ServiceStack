using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using xperters.configurations;
using xperters.infrastructure.Profiles;

namespace xperters.tests.common.Base
{
    public static class AutoMapperConfig
    {
        private static readonly object ThisLock = new object();
        private static bool _initialized;
        public static IMapper InitializedMapper { get; private set; }

        // Centralize automapper initialize
        public static void Initialize(IOptions<AppConfig> appConfig, ILoggerFactory loggerFactory)

        {
            // This will ensure one thread can access to this static initialize call
            // and ensure the mapper is reseted before initialized
            lock (ThisLock)
            {
                if (!_initialized && InitializedMapper==null)
                {
                    try{
                        Mapper.Reset();
                        Mapper.Initialize(cfg =>
                        {
                            var profile = new MappingProfile(appConfig.Value, loggerFactory);
                            
                            cfg.AddProfile(profile);
                        });

                        Mapper.Configuration.AssertConfigurationIsValid();
                        InitializedMapper = Mapper.Configuration.CreateMapper();
                    }
                    catch(InvalidOperationException ex){
                        // not interested in any exceptions here
                        // the mapper may already have been initialized - in which case continue
                    }
                    _initialized = true;
                }
            }
        }
    }
}
