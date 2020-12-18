using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using xperters.configurations;
using xperters.constants;
using xperters.entities;
using xperters.entities.Entities;
using xperters.extensions;

namespace xperters.queues.Messages
{
    public class MilestoneRequestPayerMessage : BaseMessage
    {
        public MilestoneRequestPayerMessage(AppConfig config, XpertersContext context, ILoggerFactory loggerFactory, IMapper mapper)
            :base(config, context, loggerFactory, mapper)
        {
            Config = config;
        }

        public async Task SendAsync(MilestoneRequestPayer item, string mobilePhone)
        {

            Service = new QueueService(Config, LoggerFactory, Config.ServiceBus.Queues[QueueNameConstants.MilestoneRequestPayers].Name);

            if(item.ClientId == Guid.Empty){
                throw new ArgumentOutOfRangeException("ClientId is not set");
            }

            if(mobilePhone.IsBlank()){
                throw new ArgumentOutOfRangeException("mobile number has not been specified");
            }

            await CreateRequestPayer(item.Id.ToString(), mobilePhone,  item.Amount);

            var entity = Mapper.Map<MilestoneRequestPayer>(item);
            entity.PayerStatus = null;
            Context.MilestoneRequestPayers.Add(entity);
            Context.SaveChanges();

            await Service.AddMessageAsync(entity);
            Logger.LogDebug("Added MilestoneRequestPayerMessage");
        }
        
    }
}