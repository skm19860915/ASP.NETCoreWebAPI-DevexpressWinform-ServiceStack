using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using xperters.configurations;
using xperters.constants;

namespace xperters.queues
{
    public class QueueService : IQueueService
    {

        private static IQueueClient _queueClient;
        private readonly ILogger<QueueService> _logger;

        public QueueService(AppConfig config, ILoggerFactory loggerFactory, string queueName)
        {
            _logger = loggerFactory.CreateLogger<QueueService>();
            try
            {
                string connectionString;
                if (queueName.Contains(QueueNameConstants.MilestoneRequestPayer))
                {
                    connectionString = config.ServiceBus.Queues[QueueNameConstants.MilestoneRequestPayers].ConnectionString;
                }
                else if (queueName.Contains(QueueNameConstants.MilestoneSystemRequestPayer))
                {
                    connectionString = config.ServiceBus.Queues[QueueNameConstants.MilestoneSystemRequestPayers].ConnectionString;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Unexpected queue name");
                }

                // remove the entity path section of the connection string
                int index = connectionString.LastIndexOf(";");
                if (index > 0)
                    connectionString = connectionString.Substring(0, index);

                _queueClient = new QueueClient(connectionString, queueName);

                _logger.LogDebug("Queue has been created and connected to.");
            }
            catch (Exception ex)
            {
                // log the exception to aid traceability
                _logger.LogCritical(ex.Message);

                throw;
            }
        }

        /// Add message to queue
        /// <param name="messageObject">Message to add</param>
        public async Task AddMessageAsync<T>(T messageObject)
        {
            if (messageObject != null)
            {

                var serializedMessage = JsonConvert.SerializeObject(messageObject);
                var message = new Message(Encoding.UTF8.GetBytes(serializedMessage));
                await _queueClient.SendAsync(message);

                _logger.LogDebug($"Added message {message.CorrelationId}");
            }
            else
            {
                _logger.LogWarning($"Message object is null");
            }
        }

        public async Task CompleteAsync(string lockToken)
        {
            await _queueClient.CompleteAsync(lockToken);
        }
        //
        //        public int GetMessagesCount()
        //        {
        //            var managementClient = new ManagementClient(_connectionString);
        //            var runtimeInfo = await managementClient.GetQueueRuntimeInfoAsync("queueName");
        //
        //            var messagesInQueueCount = runtimeInfo.MessageCountDetails.ActiveMessageCount;
        //        }
    }
}
