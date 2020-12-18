using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using xperters.configurations;
using xperters.constants;
using xperters.entities;
using xperters.entities.Entities;
using xperters.extensions;

namespace xperters.queues.Messages
{
    public class MilestoneSystemRequestPayerMessage : BaseMessage
    {

        public MilestoneSystemRequestPayerMessage(AppConfig config, XpertersContext context, ILoggerFactory loggerFactory, IMapper mapper)
            : base(config, context, loggerFactory, mapper)
        {

        }

        public async Task SendAsync(MilestoneSystemRequestPayer item, string mobilePhone)
        {
            Service = new QueueService(Config, LoggerFactory, Config.ServiceBus.Queues[QueueNameConstants.MilestoneSystemRequestPayers].Name);

            if(mobilePhone.IsBlank()){
                throw new ArgumentOutOfRangeException("mobile number has not been specified");
            }

            await CreateRequestPayer(item.Id.ToString(), mobilePhone,  item.Amount);

            var entity = Mapper.Map<MilestoneSystemRequestPayer>(item);
            entity.PayerStatus = null;
            Context.MilestoneSystemRequestPayers.Add(entity);
            Context.SaveChanges();

            await Service.AddMessageAsync(entity);
            Logger.LogDebug("Added MilestoneSystemRequestPayerMessage");
        }

        async Task OnMessage(Message m, CancellationToken ct)
        {
            var messageText = Encoding.UTF8.GetString(m.Body);

            Console.WriteLine("Got a message:");
            Console.WriteLine(messageText);
            Console.WriteLine($"Enqueued at {m.SystemProperties.EnqueuedTimeUtc}");

            var message = Deserialize<MilestoneSystemRequestPayer>(messageText, true);

            // Complete the message so that it is not received again.
            // This can be done only if the queue Client is created in ReceiveMode.PeekLock mode (which is the default).

            await Service.CompleteAsync(m.SystemProperties.LockToken);
        }

        Task OnException(ExceptionReceivedEventArgs args)
        {
            Console.WriteLine("Got an exception:");
            Console.WriteLine(args.Exception.Message);
            Console.WriteLine(args.ExceptionReceivedContext.ToString());
            return Task.CompletedTask;
        }

    }
}
