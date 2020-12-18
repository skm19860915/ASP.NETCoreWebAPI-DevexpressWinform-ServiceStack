using AutoMapper;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using xperters.business.Interfaces;
using xperters.constants;
using xperters.domain;
using xperters.entities.Entities;
using xperters.repositories;
using Microsoft.AspNetCore.Http;
using xperters.business.Extensions;
using xperters.enums;
using Microsoft.EntityFrameworkCore;

namespace xperters.business
{
    public class MilestoneManager : IMilestoneManager
    {
        private readonly IRepository<Milestone> _milestoneRepository;
        private readonly IRepository<MilestoneMessage> _milestoneMessageRepository;
        private readonly IRepository<MilestoneRequestPayer> _milestoneRequestPayerRepository;
        private readonly IRepository<MilestoneSystemRequestPayer> _milestoneSystemRequestPayerRepository;
        private readonly IRepository<User> _accountsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public MilestoneManager(IRepository<Milestone> milestoneRepository
                                , IRepository<MilestoneMessage> milestoneMessageRepository
                                , IRepository<MilestoneRequestPayer> milestoneRequestPayerRepository
                                , IRepository<MilestoneSystemRequestPayer> milestoneSystemRequestPayerRepository
                                , IRepository<User> accountsRepository
                                , IMapper mapper
                                , ILoggerFactory loggerFactory
                                , IHttpContextAccessor httpContextAccessor
                        )
        {
            _milestoneRepository = milestoneRepository;
            _milestoneMessageRepository = milestoneMessageRepository;
            _milestoneRequestPayerRepository = milestoneRequestPayerRepository;
            _milestoneSystemRequestPayerRepository = milestoneSystemRequestPayerRepository;
            _accountsRepository = accountsRepository;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<MilestoneManager>();
            _httpContextAccessor = httpContextAccessor;
        }

        public ResultModel<MilestoneDto> GetMilestoneDetail(Guid id)
        {
            var resultModel = new ResultModel<MilestoneDto>();
            var milestone = _milestoneRepository.Get();
            var milestoneDetail = milestone.FirstOrDefault(x => x.Id == id);
            if (milestoneDetail == null)
            {
                resultModel.Error = true;
                resultModel.Message = MessageConstants.HireJobException;
            }
            else
            {
                var milestoneDto = _mapper.Map<MilestoneDto>(milestoneDetail);
                resultModel.Data = milestoneDto;
            }
            return resultModel;
        }

        public IEnumerable<MilestoneDto> GetJobMilestones(Guid jobId)
        {
            var result = _milestoneRepository
                                .Get()
                                .Where(j=>j.Contract.JobId == jobId);

            var milestones = _mapper.Map<IEnumerable<MilestoneDto>>(result);
            return milestones;
        }

        public IEnumerable<MilestoneRequestPayerDto> GetMilestoneRequestPayers(Guid milestoneId)
        {
            var result = _milestoneRequestPayerRepository
                                        .Get()
                                        .Where(j => j.MilestoneId == milestoneId);

            var milestonRequestPayers = _mapper.Map<IEnumerable<MilestoneRequestPayerDto>>(result);
            return milestonRequestPayers;
        }

        public IEnumerable<MilestoneSystemRequestPayerDto> GetMilestoneSystemRequestPayers(Guid milestoneId)
        {
            var result = _milestoneSystemRequestPayerRepository
                                        .Get()
                                        .Where(j => j.MilestoneId == milestoneId);

            var milestonSystemRequestPayers = _mapper.Map<IEnumerable<MilestoneSystemRequestPayerDto>>(result);
            return milestonSystemRequestPayers;

        }

        public ResultModel<MilestoneMessageDto> SubmitMilestoneMessage(MilestoneMessageDto milestoneMessageDto)
        {
            var resultModel = new ResultModel<MilestoneMessageDto>();
            var loggedInUserId = _httpContextAccessor.HttpContext.User?.GetUserId();
            if (!Guid.TryParse(loggedInUserId, out var userId) || loggedInUserId==null)
            {
                _logger.LogError($"trying to parse value that is not a guid ({loggedInUserId})");
            }
            else
            {
                // mapping from domain to entity model
                var milestoneMessage = _mapper.Map<MilestoneMessage>(milestoneMessageDto);
                milestoneMessage.CreatedId = Guid.Parse(loggedInUserId);
                milestoneMessage.CreatedDate = DateTime.Now;
                milestoneMessage.ModifiedDate = DateTime.Now;
                _milestoneMessageRepository.Add(milestoneMessage);

                // GetMilestoneMessage added by user based on MilestoneId
                resultModel = GetMilestoneMessage(milestoneMessage.MilestoneId);
                resultModel.Message = MessageConstants.MilestoneSuccessful;
            }
            return resultModel;
        }

        public bool UpdateMilestoneStatus(Guid milestoneId)
        {
            var loggedInUserId = _httpContextAccessor.HttpContext.User?.GetUserId();
            if (!Guid.TryParse(loggedInUserId, out var userId) || loggedInUserId == null)
            {
                _logger.LogError($"trying to parse value that is not a guid ({loggedInUserId})");

            }
            else
            {
                // mapping from domain to entity model
                //_milestone = _mapper.Map<Milestone>(mileStoneDto);
                var data = _milestoneRepository.Get().FirstOrDefault(x => x.Id == milestoneId);
                data.MilestoneStatus = (int)Enums.MilestoneStatus.Active;
                _milestoneRepository.Update(data);
                //_milestoneMessageRepository.Add(_milestone);
                // GetMilestoneMessage added by user based on Milestoneid
                //resultModel = GetMilestoneMessage(_milestone.Id);
                //resultModel.Message = MessageConstants.MilestoneSuccessful;
            }
            return true;
        }

        public ResultModel<MilestoneMessageDto> GetMilestoneMessage(Guid milestoneId)
        {
            var resultModel = new ResultModel<MilestoneMessageDto>();
            var milestoneComments = _milestoneMessageRepository.Get();
            var users = _accountsRepository.Get();

            var commentList = milestoneComments.Where(x => x.MilestoneId == milestoneId).OrderByDescending(x => x.CreatedDate).ToList();
            // mapping from entity to domain model
            var dtos = _mapper.Map<List<MilestoneMessageDto>>(commentList);
            foreach (var item in dtos)
            {
                var data = users.FirstOrDefault(m => m.Id == item.CreatedId);
                if (data != null)
                {
                    item.CreatedBy = data.FirstName + " " + data.LastName;
                }
            }
            resultModel.DataList = dtos;
            resultModel.Error = false;
            return resultModel;
        }

        public IEnumerable<MilestonePaymentDto> GetMilestonePaymentsForApproval(int page, int numberPerPage)
        {

            int clientApproved = (int)Enums.MilestoneStatus.ClientApproved;
            var skip = numberPerPage * (page - 1);

            var results = _milestoneRepository.Get()
                                                .Include(i=>i.MilestoneRequestPayers)
                                                .Include(i => i.Contract)
                                                .Include(i => i.ContractFunds)
                                                .Include(i => i.CreatedBy)
                                                .Where(j=>j.MilestoneStatus == clientApproved)
                                                .OrderBy(x=>x.CreatedDate);

            var milestones = results.SelectMany(x => x.MilestoneRequestPayers
                    .Select(y => new MilestonePaymentDto
                    {
                        MilestoneId = x.Id,
                        MilestoneDueDate = x.DueDate,
                        MilestoneAmount = x.Amount,
                        PaymentAmount = y.Amount,
                        RequestPayerCreated = y.CreatedDate,
                        MilestoneDescription = x.MilestoneDescription,
                        MilestoneCreated = x.CreatedDate,
                        MilestoneStatus = Enums.MilestoneStatus.ClientApproved.GetDescription(),
                        LastPaymentServiceStatusCheck = y.LastPaymentServiceStatusCheck,
                        JobTitle = x.Contract.Job.JobTitle,
                        PaymentServiceCheckCount = y.PaymentServiceCheckCount
                    })
                ).Skip(skip)
                .Take(numberPerPage);

            return milestones;     
        }

        public List<Guid> UpdateMilestonePaymentsForAdminApproval(List<Guid> milestoneIdsToApprove)
        {
            var list = new List<Guid>();

            foreach (var milestoneId in milestoneIdsToApprove)
            {
                var data = _milestoneRepository.Get().FirstOrDefault(x => x.Id == milestoneId);
                if (data == null)
                    continue;

                data.MilestoneStatus = (int)Enums.MilestoneStatus.AdminApprovedFundsPaidToFreelancerWallet;
                _milestoneRepository.Update(data);
                list.Add(milestoneId);
            }

            return list;
        }
    }
}
