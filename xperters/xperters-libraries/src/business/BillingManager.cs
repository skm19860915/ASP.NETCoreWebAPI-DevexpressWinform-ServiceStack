using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using xperters.business.Extensions;
using xperters.business.Interfaces;
using xperters.constants;
using xperters.domain;
using xperters.entities.Entities;
using xperters.repositories;


namespace xperters.business
{
    public class BillingManager : IBillingManager
    {
        private readonly IRepository<Card> _cardRepository;
        private readonly IRepository<AccountDetail> _accountDetailRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IRepository<UserBalance> _userBalanceRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<UserPayment> _userPaymentRepository;
        private readonly IRepository<User> _usersRepository;

        public BillingManager(IRepository<Card> cardRepository, IMapper mapper
                            , ILoggerFactory loggerFactory, IRepository<AccountDetail> accountDetailRepository, IHttpContextAccessor httpContextAccessor, IRepository<UserBalance> userBalanceRepository
            , IRepository<UserPayment> userPaymentRepository, IRepository<User> usersRepository)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
            _accountDetailRepository = accountDetailRepository;
            _httpContextAccessor = httpContextAccessor;
            _logger = loggerFactory.CreateLogger<BillingManager>();
            _userBalanceRepository = userBalanceRepository;
            _userPaymentRepository = userPaymentRepository;
            _usersRepository = usersRepository;
        }

        public ResultModel AddAccountDetail(AccountDetailDto accountDetailDto)
        {
            var resultModel = new ResultModel();
            var accountDetail = _mapper.Map<AccountDetail>(accountDetailDto);
            if (accountDetailDto.Id == Guid.Empty)
            {
                _accountDetailRepository.Add(accountDetail);
                resultModel.Message = MessageConstants.BankDetailAdded;
            }
            else
            {
                _accountDetailRepository.Update(accountDetail);
                resultModel.Message = MessageConstants.BankDetailUpdate;
            }
            resultModel.Error = false;
            return resultModel;
        }

        public ResultModel<CardDto> AddCard(CardDto cardDto)
        {
            var resultModel = new ResultModel<CardDto>();
            try
            {
                var job = _mapper.Map<Card>(cardDto);
                // Store to the database
                _cardRepository.Add(job);
                resultModel.Error = false;
                resultModel.Message = MessageConstants.CardAddedSuccessful;
            }
            catch (Exception ex)
            {
                resultModel.Error = true;
                resultModel.Message = MessageConstants.ErrorMessageUnexpected;
            }
            return resultModel;
        }

        public ResultModel<AccountDetailDto> GetAccountDetail(Guid userId)
        {
            var resultmodel = new ResultModel<AccountDetailDto>();
            var accountDetailList = _accountDetailRepository.Get();
            var accountDetail = accountDetailList.FirstOrDefault(x => x.UserId == userId);
            if (accountDetail != null)
            {
                var dto = _mapper.Map<AccountDetailDto>(accountDetail);
                resultmodel.Data = dto;
            }
            return resultmodel;

        }

        public decimal GetBalanceByUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User?.GetUserId();
            var userBalances = _userBalanceRepository.Get(x => x.UserId == Guid.Parse(userId)).FirstOrDefault();

            if (userBalances != null)
            {
                _logger.LogDebug($"Retrieved User Ids Balance {userBalances.Balance}");
                return userBalances.Balance;
            }
            else
            {
                _logger.LogError($"User Id  {userId} not found");
                return 0;
            }

        }

        public ResultModel<xperters.models.UserPaymentView> GetUserPaymentsList(int pageNo, int pageSize)
        {
            Guid userId = Guid.Parse(_httpContextAccessor.HttpContext.User?.GetUserId());
            var userRole = _usersRepository.Get(x => x.Id == userId).FirstOrDefault().UserRole;
            var response = ApplyConditions(userId, Convert.ToString(userRole));
            var paymentHistoryList = new ResultModel<UserPaymentDto> { Data = new UserPaymentDto(), TotalCount = response.Count };
            if (pageNo != 0)
            {
                paymentHistoryList.DataList = response.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }

            var userPaymentView = new List<xperters.models.UserPaymentView>();
            foreach (var item in paymentHistoryList.DataList)
            {
                xperters.models.UserPaymentView upv = new xperters.models.UserPaymentView();
                upv.Amount = item.Amount;
                upv.Balance = item.Balance;
                upv.CurrencyId = item.CurrencyId;
                upv.FromUserId = item.FromUserId;
                upv.ToUserId = item.ToUserId;
                upv.MilestoneRequestPayerId = item.MilestoneRequestPayerId;
                upv.CreatedDate = item.CreatedDate;
                upv.PaymentId = item.Id;
                upv.PaymentTransactionTypeId = (userRole == enums.Enums.UserRole.Client) ? "Debit" : Convert.ToString(item.PaymentTransactionTypeId);

                upv.User = Convert.ToString(userFullName(item.FromUserId, Convert.ToInt16(item.PaymentTransactionTypeId), Convert.ToString(userRole), item.ToUserId));


                userPaymentView.Add(upv);
            }

            var paymentHistoryListView = new ResultModel<xperters.models.UserPaymentView> { Data = new xperters.models.UserPaymentView(), TotalCount = response.Count };
            paymentHistoryListView.DataList = userPaymentView.ToList();

            return paymentHistoryListView;

        }


        private string userFullName(Guid userId,int TransactionTypeId,string userRole, Guid ToUserId)
        {
            if (TransactionTypeId == 1 && userRole == Convert.ToString(enums.Enums.UserRole.Freelancer))
            {
                var name = _usersRepository.Get(x => x.Id == userId).FirstOrDefault();
                return name.DisplayName ;
            }
            else if(userRole == Convert.ToString(enums.Enums.UserRole.Client))
            {
                var name = _usersRepository.Get(x => x.Id == ToUserId).FirstOrDefault();
                return name.DisplayName;
            }
            return "";
        }



        private List<UserPaymentDto> ApplyConditions(Guid userId, string userRole)
        {
            var userPaymentListQuery = new List<UserPayment>();
            if (userRole == Convert.ToString(enums.Enums.UserRole.Client))
            {
                userPaymentListQuery = _userPaymentRepository.Get(x => x.FromUserId == userId).ToList();
            }
            else if (userRole == Convert.ToString(enums.Enums.UserRole.Freelancer))
            {
                userPaymentListQuery = _userPaymentRepository.Get(x => x.ToUserId == userId).ToList();
            }

            var userPaymentList = userPaymentListQuery.OrderByDescending(x => x.CreatedDate).ToList();
            var dtos = _mapper.Map<List<UserPaymentDto>>(userPaymentList);
            var paymentList = new ResultModel<UserPaymentDto> { DataList = dtos };
            var response = paymentList.DataList;

            return response;
        }

    }
}
