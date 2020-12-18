using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using RequestTelemetry = Microsoft.ApplicationInsights.DataContracts.RequestTelemetry;

namespace xperters.infrastructure.AppInsights
{
    public class SuppressHealthStaticAndBotsResourcesFilter : ITelemetryProcessor
    {
        private ITelemetryProcessor Next { get; set; }

        // some of the static resources that I'd like to exclude from my telemetry
        static readonly List<string> Names = new List<string> { ".ico", "bootstrap", "jquery", ".css", ".js" };

        // next will point to the next TelemetryProcessor in the chain.
        public SuppressHealthStaticAndBotsResourcesFilter(ITelemetryProcessor next)
        {
            Next = next;
        }

        public void Process(ITelemetry item)
        {
            // To exclude requests from our telemetry we should use RequestTelemetry
            // For dependencies, use DependencyTelemetry
            if (item is RequestTelemetry req )
            {
                // static files
                if(Names.Any(n => req.Name.Contains(n)))
                    return;

                // bot files
                if (req.Name.Contains("robots.txt", StringComparison.InvariantCultureIgnoreCase))
                    return;

                // health checks
                if (req.Name.Contains("/health", StringComparison.InvariantCultureIgnoreCase))
                    return;
            }

            // Send everything else
            Next.Process(item);
        }
    }
}
