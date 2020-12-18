using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using xperters.configurations;
using xperters.entities;
using xperters.payments.Services;
using xperters.payments.Services.Models.Internal;

namespace xperters.queues.Messages
{
    public abstract class BaseMessage
    {
        protected AppConfig Config;
        protected XpertersContext Context;
        protected ILogger Logger;
        protected readonly ILoggerFactory LoggerFactory;
        protected QueueService Service;
        protected Random Random;
        protected readonly IMapper Mapper;
        protected string MessageType;

        protected BaseMessage(AppConfig config, XpertersContext context, ILoggerFactory loggerFactory, IMapper mapper)
        {
            Config = config;
            Context = context;
            Mapper = mapper;

            LoggerFactory = loggerFactory;
            Logger = loggerFactory.CreateLogger(GetType());
        }

        protected async Task CreateRequestPayer(string externalId, string mobilePhone, decimal amount)
        {
            var currency = Config.Environment.Equals("PRD", StringComparison.InvariantCultureIgnoreCase) ? "USD" : "EUR";

            var requestPayer = new RequestPayer
            {
                Payer = new Payer
                {
                    PartyId = mobilePhone
                },
                ExternalId = externalId,
                Note = "dd",
                Message = "dd",
                Currency = currency,
                Amount = amount
            };

            var service = new MomoCollectionService(Config, LoggerFactory);
            await service.MakeRequestToPayAsync(requestPayer);
        }

        /// Deserializes JSON into object of type T.
        /// <typeparam name="T">object of type T to deserialize to</typeparam>
        /// <param name="json">JSON string</param>
        /// <param name="ignoreMissingMembersInObject">If [true] the number of fields in JSON does not have to match the number of properties in object T</param>
        /// <returns>object of type T</returns>
        protected T Deserialize<T>(string json, bool ignoreMissingMembersInObject)
        {
            var missingMemberHandling = MissingMemberHandling.Error;
            if (ignoreMissingMembersInObject)
                missingMemberHandling = MissingMemberHandling.Ignore;

            var deserializedObject = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                MissingMemberHandling = missingMemberHandling,
            });
            return deserializedObject;
        }
    }
}