using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Moq;
using xperters.infrastructure.AppInsights;
using Xunit;

namespace xperters.unit.tests.AppInsights
{
    public class AppInsightsFilterShould
    {
        private readonly Mock<ITelemetryProcessor> _telemetryProcessor;

        public AppInsightsFilterShould()
        {
            _telemetryProcessor = new Mock<ITelemetryProcessor>();
        }

        [Fact]
        public void LogNullRequest()
        {
            var request = new RequestTelemetry { Name = string.Empty };
            var filter = new SuppressHealthStaticAndBotsResourcesFilter(_telemetryProcessor.Object);

            filter.Process(request);
            _telemetryProcessor.Verify(x => x.Process(It.IsAny<ITelemetry>()), Times.Exactly(1));

        }

        [Fact]
        public void LogNormalRequest(){
            var request = new RequestTelemetry {Name = "index.htm"};
            var filter = new SuppressHealthStaticAndBotsResourcesFilter(_telemetryProcessor.Object);

            filter.Process(request);
            _telemetryProcessor.Verify(x => x.Process(It.IsAny<ITelemetry>()), Times.Exactly(1));
        }

        [Fact]
        public void IgnoreStaticFileRequest(){
            var request = new RequestTelemetry {Name = "site.js"};
            var filter = new SuppressHealthStaticAndBotsResourcesFilter(_telemetryProcessor.Object);

            filter.Process(request);
            _telemetryProcessor.Verify(x => x.Process(It.IsAny<ITelemetry>()), Times.Never);
        }

        [Fact]
        public void IgnoreRobotRequest(){
            var request = new RequestTelemetry { Name = "robots.txt" };
            var filter = new SuppressHealthStaticAndBotsResourcesFilter(_telemetryProcessor.Object);

            filter.Process(request);
            _telemetryProcessor.Verify(x => x.Process(It.IsAny<ITelemetry>()), Times.Never);
        }

        [Fact]
        public void IgnoreHealthCheckRequest(){
            var request = new RequestTelemetry { Name = "HEAD /health" };
            var filter = new SuppressHealthStaticAndBotsResourcesFilter(_telemetryProcessor.Object);

            filter.Process(request);
            _telemetryProcessor.Verify(x => x.Process(It.IsAny<ITelemetry>()), Times.Never);
        }
    }
}