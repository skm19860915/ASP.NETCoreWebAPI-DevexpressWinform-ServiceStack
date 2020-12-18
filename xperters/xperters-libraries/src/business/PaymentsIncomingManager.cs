using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using xperters.business.Extensions;
using xperters.business.Interfaces;
using xperters.domain;
using xperters.entities.Entities;
using xperters.repositories;

namespace xperters.business
{
    public class PaymentsIncomingManager : IPaymentsIncomingManager
    {
        private readonly IRepository<MilestoneRequestPayer> _milestoneRequestPayerRepository;
        private readonly IRepository<User> _userRepository;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<PaymentsIncomingManager> _logger;

        public PaymentsIncomingManager(IRepository<MilestoneRequestPayer> milestoneRequestPayerRepository,
                                IRepository<User> userRepository,
                                ILoggerFactory loggerFactory
                            )
        {
            _milestoneRequestPayerRepository = milestoneRequestPayerRepository;
            _userRepository = userRepository;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<PaymentsIncomingManager>();
        }

        public IEnumerable<PaymentIncomingDto> GetPaymentIncoming()
        {
            var userInfos = _userRepository.Get().Select(x => new
            {
                x.Id,
                x.DisplayName
            }).ToList();
            var result = _milestoneRequestPayerRepository.Get()
                                                        .Include(m => m.Milestone)
                                                        .OrderByDescending(c => c.CreatedDate);

            var list = new List<PaymentIncomingDto>();
            foreach(var item in result)
            {
                var record = new PaymentIncomingDto
                {
                    Id = item.Id,
                    MilestoneDescription = item.Milestone.MilestoneDescription,
                    UserName = userInfos.FirstOrDefault(u => u.Id == item.ClientId)?.DisplayName,
                    Currency = Utility.GetCurrencyDescription(item.CurrencyId),
                    Amount = item.Amount,
                    PayerStatus = Utility.GetRequestPayerStatusValue(item.PayerStatusId),
                    ResponseMessage = item.ResponseMessage,
                    PaymentServiceCheckCount = item.PaymentServiceCheckCount,
                    CompletedDate = item.CompletedDate,
                    LastPaymentServiceStatusCheck = item.LastPaymentServiceStatusCheck,
                    FeeFlat = item.FeeFlat,
                    FeePercent = item.FeePercent,
                    FeeTotal = item.FeeTotal,
                    TotalAmount = item.TotalAmount,
                    Created = item.CreatedDate
                };
                list.Add(record);
            }

            _logger.LogDebug("Payment Incoming list");

            return list.AsEnumerable();
        }
    }
}
