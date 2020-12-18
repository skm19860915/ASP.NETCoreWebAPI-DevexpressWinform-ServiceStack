using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using xperters.business.Extensions;
using xperters.business.Interfaces;
using xperters.domain;
using xperters.entities.Entities;
using xperters.enums;
using xperters.repositories;

namespace xperters.business
{
    public class WithdrawalsManager : IWithdrawalsManager
    {
        private readonly IRepository<UserWithdrawal> _userWithdrawalsRepository;
        private readonly IRepository<User> _userRepository;
        private readonly ILoggerFactory _loggerFactory;
        private ILogger<WithdrawalsManager> _logger;

        public WithdrawalsManager(IRepository<UserWithdrawal> userWithdrawalsRepository,
                                    IRepository<User> userRepository,
                                    ILoggerFactory loggerFactory
                                )
        {
            _userWithdrawalsRepository = userWithdrawalsRepository;
            _userRepository = userRepository;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<WithdrawalsManager>();
        }

        public IEnumerable<PaymentOutgoingDto> GetPaymentOutgoingForWithdrawals()
        {
            var userInfos = _userRepository.Get().Select(x => new
            {
                x.Id,
                x.DisplayName
            }).ToList();
            var result = _userWithdrawalsRepository.Get()
                                            .OrderByDescending(x => x.CreatedDate);

            var list = new List<PaymentOutgoingDto>();
            foreach(var item in result)
            {
                var record = new PaymentOutgoingDto()
                {
                    Id = item.Id,
                    UserName = userInfos.FirstOrDefault(u => u.Id == item.UserId).DisplayName,
                    Amount = item.Amount,
                    BalanceOld = item.BalanceOld,
                    BalanceNew = item.BalanceNew,
                    Currency = Utility.GetCurrencyDescription(item.CurrencyId),
                    PayerStatus = Utility.GetRequestPayerStatusValue(item.PayerStatusId),
                    ResponseMessage = item.ResponseMessage,
                    LastPaymentServiceStatusCheck = item.LastPaymentServiceStatusCheck,
                    PaymentServiceCheckCount = item.PaymentServiceCheckCount,
                    CompletedDate = item.CompletedDate,
                    PaymentTransactionType = item.PaymentTransactionTypeId.GetDescription(),
                    Created = item.CreatedDate
                };
                list.Add(record);
            }

            _logger.LogDebug("Payment Withdrawals list");

            return list.AsEnumerable();
        }
    }
}
