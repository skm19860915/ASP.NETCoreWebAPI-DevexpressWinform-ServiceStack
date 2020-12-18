using System;
using Xperters.Core.Logging;

namespace Xperters.Core.Configuration
{
    public abstract class AppConfigurationBase
    {
        protected readonly ILogger _log;
        protected readonly IConfigurationManager _configurationManager;

        protected AppConfigurationBase(ILogger log, IConfigurationManager configurationManager)
        {
            if (log == null)
            {
                throw new ArgumentNullException("log");
            }

            if (configurationManager == null)
            {
                throw new ArgumentNullException("configurationManager");
            }

            _log = log;
            _configurationManager = configurationManager;
        }
    }
}