using xperters.business.Interfaces;
using xperters.domain;
using xperters.repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using xperters.business.Extensions;
using xperters.constants;
using xperters.entities.Entities;
using xperters.fileutilities.Interfaces;
using xperters.enums;
using static xperters.enums.Enums;
using static xperters.enums.JobEnums;
using xperters.models;
using SendGrid;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using xperters.email.Interface;
using xperters.extensions;
using xperters.models.DataViews;
using xperters.models.DataViews.AdminJob;

namespace xperters.business
{
    public class JobManager : IJobManager
    {
        private readonly IRepository<Job> _jobRepository;
        private readonly IRepository<Skill> _skillsRepository;
        private readonly IRepository<Category> _categoriesRepository;
        private readonly IRepository<JobAttachment> _attachmentsRepository;
        private readonly IRepository<JobBid> _jobBidRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IAttachmentHandler<JobDto> _attachmentHandler;
        private readonly IAttachmentHandler<JobBidDto> _jobBidAttachmentHandler;
        private readonly IHttpFileHandler _httpFileHandler;
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<JobBidAttachment> _jobBidAttachmentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<JobBidChatSession> _jobBidChatSessionsRepository;
        private readonly IRepository<JobBidChatSessionUser> _jobBidChatSessionUsersRepository;
        private readonly IRepository<JobBidChatMessage> _jobBidChatMessagesRepository;

        private readonly IRepository<ContractChatSession> _contractChatSessionsRepository;
        private readonly IRepository<ContractChatSessionUser> _contractChatSessionUsersRepository;
        private readonly IRepository<ContractChatMessage> _contractChatMessageRepository;
        private readonly IRepository<JobContract> _jobContractRepository;
        private readonly IRepository<Milestone> _milestoneRepository;
        private readonly IRepository<ContractMilestoneFund> _contractFundRepository;
        private readonly IAttachmentHandler<MilestoneDto> _milestoneAttachmentHandler;
        private readonly IRepository<MilestoneAttachment> _milestoneAttachmentRepository;
        private readonly IManageEmails _emailManager;
        private readonly IRepository<EmailAudit> _emailAuditRepository;



        public JobManager(IRepository<Job> jobRepository
                            , IRepository<Skill> skillsRepository
                            , IRepository<Category> categoriesRepository
                            , IRepository<JobAttachment> attachmentsRepository
                            , IRepository<JobBid> jobBidRepository
                            , IAttachmentHandler<JobDto> attachmentHandler
                            , IAttachmentHandler<JobBidDto> jobBidAttachmentHandler
                            , IHttpFileHandler httpFileHandler
                            , IMapper mapper
                            , ILoggerFactory loggerFactory
                            , IRepository<User> usersRepository
                            , IRepository<JobBidAttachment> jobBidAttachmentRepository
                            , IHttpContextAccessor httpContextAccessor
                            , IRepository<JobBidChatSession> jobBidChatSessionsRepository
                            , IRepository<JobBidChatSessionUser> jobBidChatSessionUsersRepository
                            , IRepository<JobBidChatMessage> jobBidChatMessagesRepository
                            , IRepository<ContractChatSession> contractChatSessionsRepository
                            , IRepository<ContractChatSessionUser> contractChatSessionUsersRepository
                            , IRepository<ContractChatMessage> contractChatMessageRepository
                            , IRepository<JobContract> jobContractRepository
                            , IRepository<Milestone> milestoneRepository
                            , IRepository<ContractMilestoneFund> contractFundRepository
                            , IAttachmentHandler<MilestoneDto> milestoneAttachmentHandler
                            , IRepository<MilestoneAttachment> milestoneAttachmentRepository
                            , IManageEmails emailManager
                            , IRepository<EmailAudit> emailAuditRepository
        )
        {
            _jobRepository = jobRepository;
            _skillsRepository = skillsRepository;
            _categoriesRepository = categoriesRepository;
            _attachmentsRepository = attachmentsRepository;
            _jobBidRepository = jobBidRepository;
            _attachmentHandler = attachmentHandler;
            _jobBidAttachmentHandler = jobBidAttachmentHandler;
            _httpFileHandler = httpFileHandler;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<JobManager>();
            _usersRepository = usersRepository;
            _jobBidAttachmentRepository = jobBidAttachmentRepository;
            _httpContextAccessor = httpContextAccessor;
            _jobBidChatSessionUsersRepository = jobBidChatSessionUsersRepository;
            _jobBidChatSessionsRepository = jobBidChatSessionsRepository;
            _jobBidChatMessagesRepository = jobBidChatMessagesRepository;
            _contractChatSessionsRepository = contractChatSessionsRepository;
            _contractChatSessionUsersRepository = contractChatSessionUsersRepository;
            _contractChatMessageRepository = contractChatMessageRepository;
            _jobContractRepository = jobContractRepository;
            _milestoneRepository = milestoneRepository;
            _contractFundRepository = contractFundRepository;
            _milestoneAttachmentHandler = milestoneAttachmentHandler;
            _milestoneAttachmentRepository = milestoneAttachmentRepository;
            _emailManager = emailManager;
            _emailAuditRepository = emailAuditRepository;

        }

        public ResultModel<JobDto> GetJobList(int pageNo, List<SearchFilter> searchFilter, int pageSize)
        {
            //throw new Exception();
            var response = ApplyConditions(searchFilter);
            var jobList = new ResultModel<JobDto> { Data = new JobDto(), TotalCount = response.Count };
            if (pageNo != 0)
            {
                jobList.DataList = response.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

            }
            return jobList;

        }

        //Code is created for apply the filter on the manager side
        private List<JobDto> ApplyConditions(List<SearchFilter> searchFilter)
        {
            int categoryId;
            var jobsListQuery = _jobRepository.Get(x => x.JobStatusId == enums.JobEnums.JobStatus.JobPosted.GetEnumValue());

            if (searchFilter != null)
            {
                var searchText = searchFilter.FirstOrDefault(x => x.FilterType == JobConstants.SearchText);
                var category = searchFilter.FirstOrDefault(x => x.FilterType == JobConstants.Category);
                if (searchText != null)
                {
                    //we need to remove the .ToList once we make the query queryable for all 
                    jobsListQuery = jobsListQuery.Where(x => x.JobTitle.ToLower().Contains(searchText.Values.ToLower()));
                }
                if (category != null)
                {
                    categoryId = Convert.ToInt32(category.Values);
                    jobsListQuery = jobsListQuery.Where(x => x.SelectedJobCategory == categoryId);
                }
            }

            var jobsList = jobsListQuery.OrderByDescending(x => x.CreatedDate).ToList();
            var dtos = _mapper.Map<List<JobDto>>(jobsList);
            var jobList = new ResultModel<JobDto> { DataList = dtos };
            var response = jobList.DataList;

            return response;
        }

        public ResultModel<JobDto> AddJob(JobDto jobDto, UserDto userDto)
        {
            var resultModel = new ResultModel<JobDto>();
            if (_httpFileHandler != null)
            {
                var httpFiles = _httpFileHandler.GetFromFiles();

                if (httpFiles.Any())
                {
                    _attachmentHandler.ConvertFromWebFormToDto(jobDto, httpFiles);
                }
            }

            // populate the mandatory fields
            jobDto.UserId = userDto.Id;
            jobDto.Id = Guid.NewGuid();
            jobDto.JobStatusId = enums.JobEnums.JobStatus.JobPosted.GetEnumValue();

            // Use the mandatory fields to store to blob
            _attachmentHandler?.StoreAttachmentsToBlob(jobDto);
            var job = _mapper.Map<Job>(jobDto);
            var attachmentList = new List<JobAttachment>();
            foreach (var attachment in jobDto.JobAttachments)
            {
                var jobAttachment = new JobAttachment()
                {
                    Id = attachment.Id,
                    JobId = attachment.JobId,
                    FileName = attachment.FileName,
                    CreatedDate = attachment.CreatedDate,
                    MimeType = attachment.MimeType,
                    FileSize = attachment.FileSize,
                    LocalPath = attachment.LocalPath,
                    Uri = attachment.Uri,
                    ModifiedDate = attachment.ModifiedDate
                };
                attachmentList.Add(jobAttachment);
            }
            job.JobAttachments = attachmentList;

            // Store to the database

            _jobRepository.Add(job);
            // Map back so that the new id values
            // are returned.

            // getting no of attachments
            resultModel.TotalCount = jobDto.JobAttachments.Count();
            resultModel.Data = jobDto;

            return resultModel;
        }

        public JobDto GetJobById(Guid jobId)
        {
            var job = _jobRepository.Get(jobId);

            var dto = _mapper.Map<JobDto>(job);

            if (dto != null)
            {
                _logger.LogDebug($"Retrieved job {job.Id}");
            }
            return dto;
        }

        public Guid GetJobIdByContractId(Guid contractId)
        {
            var jobContract = _jobContractRepository.Get(contractId);

            if (jobContract != null)
            {
                _logger.LogDebug($"Retrieved contact {jobContract.Id}");
            }
            else
            {
                _logger.LogError($"Job contract  {contractId} not found");
            }
            return jobContract.JobId;
        }

        public Guid GetFreelancerIdByJobId(Guid jobId)
        {
            var jobContract = _jobContractRepository.Get(x => x.JobId == jobId).FirstOrDefault();

            if (jobContract != null)
            {
                _logger.LogDebug($"Retrieved contact {jobContract.Id}");
                return jobContract.FreelancerId;
            }
            else
            {
                _logger.LogError($"Job Id  {jobId} not found");
                return Guid.Empty;
            }

        }

        public ResultModel<SkillDto> GetSkills()
        {
            var result = new ResultModel<SkillDto>();
            var skillsList = _skillsRepository.Get();

            var dto = _mapper.Map<List<SkillDto>>(skillsList);
            result.DataList = dto;
            result.TotalCount = dto.Count;
            return result;
        }

        public ResultModel<CategoryDto> GetCategories()
        {
            var result = new ResultModel<CategoryDto>();
            var categories = _categoriesRepository.Get();

            var dtos = _mapper.Map<List<CategoryDto>>(categories);

            result.DataList = dtos;
            result.TotalCount = dtos.Count;
            return result;
        }

        public JobAttachmentDto GetAttachment(Guid id)
        {
            var entity = _attachmentsRepository.Get(id);
            var attachment = _mapper.Map<JobAttachmentDto>(entity);
            return attachment;
        }

        public List<JobAttachmentDto> GetAttachments(Guid jobId)
        {
            var attachmentList = _attachmentsRepository.Get(cond => cond.JobId == jobId).ToList();
            var dtos = _mapper.Map<List<JobAttachmentDto>>(attachmentList);
            return dtos;
        }

        public List<JobBidChatMessageDto> GetChatsMessage(Guid id, Guid clientId, int messageType, Guid freelancerId)
        {

            Guid chatSessionId;
            if (freelancerId == Guid.Empty)
            {
                var chatJobContract = _jobContractRepository.Get(x => x.JobId == id).FirstOrDefault();

                if (chatJobContract == null)
                {
                    chatJobContract.FreelancerId = Guid.Empty;
                }

                var chatSessionsList = _jobBidChatSessionsRepository.Get(x => x.ClientId == clientId &&
                                                                         x.FreelancerId == chatJobContract.FreelancerId &&
                                                                         x.JobId == id).FirstOrDefault();
                if (chatSessionsList != null)
                {
                    chatSessionId = chatSessionsList.Id;
                    freelancerId = chatJobContract.FreelancerId;
                }
                else
                {
                    chatSessionId = Guid.Empty;
                    freelancerId = Guid.Empty;
                }
            }
            else
            {
                var chatSessionsList = _jobBidChatSessionsRepository.Get(x => x.ClientId == clientId && x.FreelancerId == freelancerId && x.JobId == id).FirstOrDefault();
                if (chatSessionsList == null)
                {
                    chatSessionId = Guid.Empty;
                }
                else
                {
                    chatSessionId = chatSessionsList.Id;
                }
            }
            var messageListQuery = _jobBidChatMessagesRepository.Get(cond => cond.MessageType == messageType && cond.JobBidChatSessionId == chatSessionId).ToList();
            var messageList = messageListQuery.OrderBy(x => x.CreatedDate).ToList();

            var dtos = _mapper.Map<List<JobBidChatMessageDto>>(messageList);
            var messagesList = new ResultModel<JobBidChatMessageDto> { DataList = dtos };
            var response = messagesList.DataList;
            return response;
        }




        public JobBidAttachmentDto GetJobBidAttachment(Guid id)
        {
            var bidAttachment = _jobBidAttachmentRepository.Get(id);
            var attachment = _mapper.Map<JobBidAttachmentDto>(bidAttachment);
            return attachment;

        }

        public JobBidDto GetHiredBid(Guid jobId, Guid userId)
        {
            var datalist = _jobBidRepository.Get();

            var data = datalist.FirstOrDefault(x => x.FreelancerUserId == userId && x.JobId == jobId);
            var jobList = _jobRepository.Get();
            var job = jobList.FirstOrDefault(x => x.Id == jobId);

            var userList = _usersRepository.Get();
            var user = userList.FirstOrDefault(x => x.Id == userId);

            var jobBid = _mapper.Map<JobBidDto>(data);
            jobBid.Job = _mapper.Map<JobDto>(job);
            jobBid.User = _mapper.Map<UserDto>(user);

            return jobBid;
        }

        public UserDto GetUserDetails(Guid userId)
        {
            var userDetails = _usersRepository.Get(x => x.Id == userId).FirstOrDefault();
            var user = _mapper.Map<UserDto>(userDetails);
            return user;
        }


        public ResultModel SubmitBid(JobBidDto jobBidDto)
        {
            var resultModel = new ResultModel();
            if (jobBidDto.JobId != Guid.Empty)
            {
                jobBidDto.Job = GetJobById(jobBidDto.JobId);

                if (jobBidDto.Id != Guid.Empty && jobBidDto.JobId != Guid.Empty)
                {
                    if (_httpFileHandler != null)
                    {
                        var httpFiles = _httpFileHandler.GetFromFiles();

                        if (httpFiles.Any())
                        {
                            _jobBidAttachmentHandler.ConvertFromWebFormToDto(jobBidDto, httpFiles);
                        }
                    }
                    _jobBidAttachmentHandler?.StoreAttachmentsToBlob(jobBidDto);
                    var jobBid = _mapper.Map<JobBid>(jobBidDto);

                    if (jobBid.JobBidAttachments.Count != 0)
                    {
                        List<JobBidAttachment> jobBidAttachments = new List<JobBidAttachment>();
                        foreach (var attachments in jobBid.JobBidAttachments)
                        {
                            attachments.JobBidId = jobBid.Id;
                            jobBidAttachments.Add(attachments);
                        }
                        _jobBidAttachmentRepository.AddList(jobBidAttachments);
                    }
                    _jobBidRepository.Update(jobBid);
                }
                else
                {
                    var userId = _httpContextAccessor.HttpContext.User?.GetUserId();

                    if (!Guid.TryParse(userId, out var freelancerUserId))
                    {
                        resultModel.Error = true;
                        resultModel.Message = JobBidConstants.BidSubmissionFailed;
                        return resultModel;
                    }

                    jobBidDto.FreelancerUserId = freelancerUserId;

                    var data = _jobBidRepository.Get().Where(x => x.FreelancerUserId == jobBidDto.FreelancerUserId && x.JobId == jobBidDto.JobId).ToList();

                    if (data.Count == 0)
                    {
                        //Contractor himself cannot submit the bid for his job
                        if (jobBidDto.Job.UserId != jobBidDto.FreelancerUserId)
                        {
                            if (_httpFileHandler != null)
                            {
                                var httpFiles = _httpFileHandler.GetFromFiles();

                                if (httpFiles.Any())
                                {
                                    _jobBidAttachmentHandler.ConvertFromWebFormToDto(jobBidDto, httpFiles);
                                }
                            }

                            jobBidDto.Id = Guid.NewGuid();
                            _jobBidAttachmentHandler?.StoreAttachmentsToBlob(jobBidDto);
                            var jobBid = _mapper.Map<JobBid>(jobBidDto);
                            _jobBidRepository.Add(jobBid);
                            foreach (var attachment in jobBidDto.JobBidAttachments)
                            {
                                var x = (from a in jobBid.JobBidAttachments
                                         from b in jobBidDto.JobBidAttachments
                                         where a.FileName == b.FileName
                                         select a.Id).First();

                                attachment.Id = x;
                            }
                            resultModel.TotalCount = jobBidDto.JobBidAttachments.Count();
                            resultModel.Error = false;
                            resultModel.Message = JobBidConstants.BidSubmitted;

                        }
                        else
                        {
                            resultModel.Error = true;
                            resultModel.Message = JobBidConstants.BidSubmissionFailed;
                        }
                    }
                    else
                    {
                        resultModel.Error = true;
                        resultModel.Message = JobBidConstants.BidAlreadySubmitted;
                    }
                }
            }
            else
            {
                resultModel.Error = true;
                resultModel.Message = JobBidConstants.InvalidBid;
            }
            return resultModel;
        }

        public ResultModel<JobDto> GetMyJobList(int pageNo, int pageSize, int jobStatus)
        {
            var jobList = new ResultModel<JobDto>();

            var loggedInUserId = _httpContextAccessor.HttpContext.User?.GetUserId();
            var data = _jobRepository.Get();

            if (!Guid.TryParse(loggedInUserId, out var userId))
            {
                jobList.Error = true;
                jobList.Message = JobBidConstants.BidSubmissionFailed;
                return jobList;
            }

            //Expression<Func<Job, bool>> cond = x => x.UserId == userId ;

            var response = data.Where(x => x.UserId == userId && x.JobStatusId == jobStatus).OrderByDescending(y => y.CreatedDate).ToList();
            jobList.TotalCount = response.Count;

            if (pageNo != 0)
            {
                response = response.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                var dtos = _mapper.Map<List<JobDto>>(response);
                jobList.DataList = dtos;
            }
            return jobList;
        }


        //Date21feb19

        public ResultModel<JobBidDto> GetBidsByJobId(Guid jobId, int status)
        {

            var jobbidList = new ResultModel<JobBidDto>();
            var jobList = _jobRepository.Get();
            var job = jobList.Where(x => x.Id == jobId).FirstOrDefault();
            if (job != null)
            {
                var jobBids = new List<JobBid>();
                var BidList = _jobBidRepository.Get();
                if (status == JobBidStatus.BidSelected.GetEnumValue())
                {
                    jobBids = BidList.Where(x => x.JobId == job.Id && x.BidStatus == status).ToList();

                }
                else
                {
                    jobBids = BidList.Where(x => x.JobId == job.Id && x.BidStatus != JobBidStatus.BidSelected.GetEnumValue()).ToList();
                }
                var userList = _usersRepository.Get();
                var bidUser = userList.Where(x => jobBids.Select(col => col.FreelancerUserId).Contains(x.Id)).ToList();
                var bidAttachmentList = _jobBidAttachmentRepository.Get();
                var jobBidsAttachment = bidAttachmentList.Where(x => jobBids.Select(col => col.Id).Contains(x.JobBidId)).ToList();
                var jobBidDtos = new List<JobBidDto>();

                foreach (var item in jobBids)
                {
                    var bidDto = new JobBidDto
                    {
                        BidAmount = item.BidAmount,
                        Message = item.Message,
                        Id = item.Id,
                        FreelancerUserId = item.FreelancerUserId,
                        User = _mapper.Map<UserDto>(bidUser.FirstOrDefault(filter => filter.Id == item.FreelancerUserId)),
                        Job = _mapper.Map<JobDto>(job),
                        JobBidAttachments = _mapper.Map<List<JobBidAttachmentDto>>(jobBidsAttachment)
                    };
                    jobBidDtos.Add(bidDto);
                }
                jobbidList.DataList = jobBidDtos;
            }

            return jobbidList;
        }
        public ResultModel HireFreeLancer(HiredJobDto HiredJobDto)
        {
            ResultModel resultModel = new ResultModel();
            var jobBidList = _jobBidRepository.Get();
            var jobBid = jobBidList.FirstOrDefault(x => x.JobId == HiredJobDto.JobId && x.FreelancerUserId == HiredJobDto.FreelancerId);

            var jobList = _jobRepository.Get();
            var job = jobList.FirstOrDefault(x => x.Id == HiredJobDto.JobId);

            try
            {
                if (jobBid.BidStatus != JobBidStatus.BidSelected.GetEnumValue() && job.JobStatusId != enums.JobEnums.JobStatus.JobInProgress.GetEnumValue())
                {
                    HiredJobDto.Amount = jobBid.BidAmount;
                    jobBid.BidStatus = JobBidStatus.BidSelected.GetEnumValue();
                    jobBid.ModifiedDate = HiredJobDto.ModifiedDate;
                    _jobBidRepository.Update(jobBid);

                    job.JobStatusId = enums.JobEnums.JobStatus.JobInProgress.GetEnumValue();
                    job.ModifiedDate = HiredJobDto.ModifiedDate;
                    _jobRepository.Update(job);

                    var contractChatSessionList = _contractChatSessionsRepository.Get(x => x.FreelancerId == HiredJobDto.FreelancerId && x.JobId == HiredJobDto.JobId && x.ClientId == HiredJobDto.ClientId).FirstOrDefault();

                    if (contractChatSessionList == null)
                    {
                        var contractChatSession = _mapper.Map<ContractChatSession>(HiredJobDto);
                        _contractChatSessionsRepository.Add(contractChatSession);
                        HiredJobDto.ContractChatSessionId = contractChatSession.Id;
                    }
                    else
                    {
                        HiredJobDto.ContractChatSessionId = contractChatSessionList.Id;
                    }

                    var contractChatUser = _contractChatSessionUsersRepository.Get(x => x.ContractChatSessionId == HiredJobDto.ContractChatSessionId && x.UserId == HiredJobDto.ClientId).FirstOrDefault();

                    if (contractChatUser == null)
                    {
                        var contractChatSessionUser = _mapper.Map<ContractChatSessionUser>(HiredJobDto);
                        _contractChatSessionUsersRepository.Add(contractChatSessionUser);
                    }

                    var contractChatMessage = _mapper.Map<ContractChatMessage>(HiredJobDto);
                    _contractChatMessageRepository.Add(contractChatMessage);

                    var jobContractItem = _mapper.Map<JobContract>(HiredJobDto);
                    _jobContractRepository.Add(jobContractItem);

                    resultModel.Error = false;
                    resultModel.Message = MessageConstants.HireJobSuccessful;
                    return resultModel;
                }
                else
                {
                    resultModel.Error = true;
                    resultModel.Message = MessageConstants.AlreadyHired;
                    return resultModel;
                }
            }
            catch (Exception ex)
            {

                resultModel.Error = true;
                resultModel.Message = MessageConstants.HireJobException;
                return resultModel;
            }

        }

        public ResultModel<JobBidChatMessageDto> SaveChatMessage(JobBidChatMessageView jobBidChatMessageView)
        {

            var resultModel = new ResultModel<JobBidChatMessageDto>();
            if (jobBidChatMessageView.JobId == Guid.Empty && jobBidChatMessageView.SenderId == Guid.Empty)
            {
                resultModel.Error = true;
                resultModel.Message = MessageConstants.JobBidChatMessagesError;
                return resultModel;
            }

            var contractSession = _contractChatSessionsRepository.Get(x => x.JobId == jobBidChatMessageView.JobId && x.ClientId == jobBidChatMessageView.SenderId).FirstOrDefault();

            var chatSessionsList = _jobBidChatSessionsRepository.Get(x => x.ClientId == jobBidChatMessageView.SenderId &&
                                                                          x.FreelancerId == contractSession.FreelancerId &&
                                                                          x.JobId == jobBidChatMessageView.JobId).FirstOrDefault();
            if (chatSessionsList == null)
            {
                var chatSessionsDto = _mapper.Map<JobBidChatSessionDto>(jobBidChatMessageView);
                chatSessionsDto.FreelancerId = contractSession.FreelancerId;
                chatSessionsDto.ClientId = jobBidChatMessageView.SenderId;

                var chatSessions = _mapper.Map<JobBidChatSession>(chatSessionsDto);
                _jobBidChatSessionsRepository.Add(chatSessions);
                jobBidChatMessageView.JobBidChatSessionId = chatSessions.Id;
            }
            else
            {
                jobBidChatMessageView.JobBidChatSessionId = chatSessionsList.Id;
            }

            if (jobBidChatMessageView.JobBidChatSessionId == Guid.Empty)
            {
                resultModel.Error = true;
                resultModel.Message = MessageConstants.JobBidChatMessagesError;
                return resultModel;
            }

            var jobDto = _mapper.Map<JobBidChatMessageDto>(jobBidChatMessageView);

            var chatSessionsUserList = _jobBidChatSessionUsersRepository.Exists(x => x.JobBidChatSessionId == jobBidChatMessageView.JobBidChatSessionId &&
                                                                                    x.UserId == contractSession.FreelancerId);
            if (!chatSessionsUserList)
            {

                var chatSessionsUser = _mapper.Map<JobBidChatSessionUser>(jobDto);
                chatSessionsUser.UserId = jobDto.SenderId;
                _jobBidChatSessionUsersRepository.Add(chatSessionsUser);
            }
            var chatMessagesList = _mapper.Map<JobBidChatMessage>(jobDto);

            if (jobBidChatMessageView.SenderType == enums.Enums.SenderType.Client)
            {
                resultModel.Message = MessageConstants.JobBidChatMessages;
            }
            else if (jobBidChatMessageView.SenderType == enums.Enums.SenderType.Freelancer)
            {
                chatMessagesList.SenderId = contractSession.FreelancerId;
                resultModel.Message = MessageConstants.JobBidChatMessagesClient;
            }
            else if (jobBidChatMessageView.SenderType == enums.Enums.SenderType.Admin)
            {
                resultModel.Message = MessageConstants.JobBidChatMessages;
            }
            _jobBidChatMessagesRepository.Add(chatMessagesList);


            resultModel.Error = false;
            return resultModel;
        }


        public ResultModel<BidNegotiationDto> BidNegotiation(BidNegotiationDto bidNegotiationDto)
        {
            var resultModel = new ResultModel<BidNegotiationDto>();
            if (bidNegotiationDto.JobId == Guid.Empty &&
                bidNegotiationDto.ClientId == Guid.Empty &&
                bidNegotiationDto.FreelancerId == Guid.Empty)
            {
                resultModel.Error = true;
                resultModel.Message = MessageConstants.JobBidChatMessagesError;
                return resultModel;
            }

            var chatSessionsList = _jobBidChatSessionsRepository.Get(x => x.ClientId == bidNegotiationDto.ClientId &&
                                                                          x.FreelancerId == bidNegotiationDto.FreelancerId &&
                                                                          x.JobId == bidNegotiationDto.JobId).FirstOrDefault();
            if (chatSessionsList == null)
            {
                var chatSessions = _mapper.Map<JobBidChatSession>(bidNegotiationDto);
                _jobBidChatSessionsRepository.Add(chatSessions);
                bidNegotiationDto.JobBidChatSessionId = chatSessions.Id;
            }
            else
            {
                bidNegotiationDto.JobBidChatSessionId = chatSessionsList.Id;
            }

            if (bidNegotiationDto.JobBidChatSessionId == Guid.Empty)
            {
                resultModel.Error = true;
                resultModel.Message = MessageConstants.JobBidChatMessagesError;
                return resultModel;
            }

            var chatSessionsUserList = _jobBidChatSessionUsersRepository.Exists(x => x.JobBidChatSessionId == bidNegotiationDto.JobBidChatSessionId &&
                                                                                     x.UserId == bidNegotiationDto.ClientId);
            if (!chatSessionsUserList)
            {

                var chatSessionsUser = _mapper.Map<JobBidChatSessionUser>(bidNegotiationDto);
                chatSessionsUser.UserId = bidNegotiationDto.ClientId;
                _jobBidChatSessionUsersRepository.Add(chatSessionsUser);
            }

            var chatMessagesList = _mapper.Map<JobBidChatMessage>(bidNegotiationDto);

            if (bidNegotiationDto.SenderType == enums.Enums.SenderType.Client)
            {
                chatMessagesList.SenderId = bidNegotiationDto.ClientId;
            }
            else if (bidNegotiationDto.SenderType == enums.Enums.SenderType.Freelancer)
            {
                chatMessagesList.SenderId = bidNegotiationDto.FreelancerId;
            }
            else if (bidNegotiationDto.SenderType == enums.Enums.SenderType.Admin)
            {
                chatMessagesList.SenderId = bidNegotiationDto.ClientId;
            }

            _jobBidChatMessagesRepository.Add(chatMessagesList);
            var jobBidList = _jobBidRepository.Get();
            var jobBid = jobBidList.FirstOrDefault(x => x.JobId == bidNegotiationDto.JobId && x.FreelancerUserId == bidNegotiationDto.FreelancerId);
            if (jobBid == null)
            {
                resultModel.Error = true;
                resultModel.Message = MessageConstants.JobBidChatMessagesError;
                return resultModel;
            }
            else if (jobBid != null)
            {

                if (jobBid.BidStatus == JobBidStatus.BidsSubmitted.GetEnumValue())
                {
                    jobBid.BidStatus = JobBidStatus.BidAmendment.GetEnumValue();
                    jobBid.ModifiedDate = bidNegotiationDto.ModifiedDate;

                }
                _jobBidRepository.Update(jobBid);
            }
            resultModel.Error = false;
            if (bidNegotiationDto.SenderType == enums.Enums.SenderType.Client)
            {
                resultModel.Message = MessageConstants.JobBidChatMessages;
            }
            else if (bidNegotiationDto.SenderType == enums.Enums.SenderType.Freelancer)
            {
                resultModel.Message = MessageConstants.JobBidChatMessagesClient;
            }
            else if (bidNegotiationDto.SenderType == enums.Enums.SenderType.Admin)
            {
                resultModel.Message = MessageConstants.JobBidChatMessages;
            }
            return resultModel;
        }


        public CountModel GetCount(Guid id)
        {
            CountModel countModel = new CountModel();
            var jobList = _jobRepository.Get();
            var job = jobList.Where(x => x.Id == id).FirstOrDefault();
            if (job != null)
            {
                var Bid = _jobBidRepository.Get(x => x.JobId == job.Id && x.BidStatus != JobBidStatus.BidSelected.GetEnumValue()).ToList();
                var Hired = _jobBidRepository.Get(x => x.JobId == job.Id && x.BidStatus == JobBidStatus.BidSelected.GetEnumValue()).ToList();
                countModel.BidList = Bid.Count;
                countModel.HiredList = Hired.Count;
                countModel.error = false;
            }
            return countModel;
        }

        #region MyJobsCount

        public JobDto GetMyJobCount()
        {

            var loggedInUserId = _httpContextAccessor.HttpContext.User?.GetUserId();
            if (!Guid.TryParse(loggedInUserId, out var userId))
            {

            }
            var data = _jobRepository.Get().ToList();
            var PostedjObCount = data.Where(x => x.UserId == userId && x.JobStatusId == enums.JobEnums.JobStatus.JobPosted.GetEnumValue()).Count();
            var InprogresJobcount = data.Where(x => x.UserId == userId && x.JobStatusId == enums.JobEnums.JobStatus.JobInProgress.GetEnumValue()).Count();
            var CompletedJobcount = data.Where(x => x.UserId == userId && x.JobStatusId == enums.JobEnums.JobStatus.JobCompleted.GetEnumValue()).Count();
            var CanceledJobcount = data.Where(x => x.UserId == userId && x.JobStatusId == enums.JobEnums.JobStatus.JobCanceled.GetEnumValue()).Count();
            var job = new JobDto()
            {
                PostJobCount = PostedjObCount,
                InprogressJobCount = InprogresJobcount,
                CompletedJobCount = CompletedJobcount,
                CanceledJobCount = CanceledJobcount
            };
            return job;
        }
        #endregion


        #region Milestone

        public ResultModel<MilestoneDto> SubmitMilestone(MilestoneDto milestoneDto)
        {
            var resultModel = new ResultModel<MilestoneDto>();
            var loggedInUserId = _httpContextAccessor.HttpContext.User?.GetUserId();
            if (!Guid.TryParse(loggedInUserId, out var userId))
            {
                var message = $"No logged in user found: {loggedInUserId}";
                _logger.LogCritical(message);
                throw new ArgumentException(message);
            }

            var contract = _jobContractRepository.Get();
            var contractId = contract.Where(x => x.JobId == milestoneDto.ContractId).FirstOrDefault();
            if (contractId == null)
            {
                resultModel.Error = true;
                resultModel.Message = MessageConstants.HireJobException;
            }
            else
            {
                milestoneDto.ContractId = contractId.Id;
                milestoneDto.CreatedId = userId;

                var milestone = _mapper.Map<Milestone>(milestoneDto);

                // funds need to be added to the milestone.  
                milestone.MilestoneStatus = (int)enums.Enums.MilestoneStatus.AddFunds;
                _milestoneRepository.Add(milestone);

                var getLastRecord = _milestoneRepository.Get()
                                                        .OrderByDescending(x => x.CreatedDate)
                                                        .FirstOrDefault();

                resultModel.Error = false;
                resultModel.Message = MessageConstants.MilestoneSuccessful;
                resultModel.Id = getLastRecord.Id;
            }
            return resultModel;
        }

        public ResultModel<MilestoneDto> GetMilestone(int pageNo, int pageSize, Guid jobId)
        {
            var resultModel = new ResultModel<MilestoneDto>();
            var contract = _jobContractRepository.Get();
            var contractId = contract.Where(x => x.JobId == jobId).FirstOrDefault();
            if (contractId == null)
            {
                resultModel.Error = true;
                resultModel.Message = MessageConstants.HireJobException;
            }
            else
            {
                var milestoneList = _milestoneRepository.Get();
                var milestone = milestoneList.Where(x => x.ContractId == contractId.Id).OrderByDescending(x => x.CreatedDate).ToList();

                var attachments = _milestoneAttachmentRepository.Get(x => milestone.Select(col => col.Id).Contains(x.MilestoneId)).ToList();

                resultModel.TotalCount = milestone.Count;
                if (pageNo != 0)
                {
                    milestone = milestone.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                    var dtos = _mapper.Map<List<MilestoneDto>>(milestone);
                    foreach (var item in dtos)
                    {
                        if (attachments.Count != 0)
                        {
                            List<MilestoneAttachmentDto> milestoneAttachments = new List<MilestoneAttachmentDto>();
                            foreach (var file in attachments)
                            {
                                if (file.MilestoneId == item.Id)
                                {
                                    milestoneAttachments.Add(_mapper.Map<MilestoneAttachmentDto>(file));
                                }
                            }
                            item.MilestoneAttachments = milestoneAttachments;
                        }
                    }
                    resultModel.DataList = dtos;
                }
            }
            return resultModel;
        }

        #endregion
        public ResultModel<JobBidDto> GetMyBids(Guid userId)
        {
            var BidList = new ResultModel<JobBidDto>();
            var jobList = _jobRepository.Get();
            var userList = _usersRepository.Get();
            var user = userList.FirstOrDefault(x => x.Id == userId);
            var Bidlist = _jobBidRepository.Get();
            var MyBids = Bidlist.Where(x => x.FreelancerUserId == userId && x.BidStatus != JobBidStatus.BidSelected.GetEnumValue()).ToList();
            var job = _jobRepository.Get(x => MyBids.Select(col => col.JobId).Contains(x.Id) && x.JobStatusId != enums.JobEnums.JobStatus.JobInProgress.GetEnumValue()).ToList();

            var jobBidDtos = new List<JobBidDto>();
            if (job.Count != 0)
            {
                foreach (var item in MyBids)
                {
                    var bidDto = new JobBidDto
                    {
                        BidAmount = item.BidAmount,
                        Message = item.Message,
                        Id = item.Id,
                        FreelancerUserId = item.FreelancerUserId,
                        User = _mapper.Map<UserDto>(user),
                        Job = _mapper.Map<JobDto>(job.FirstOrDefault(filter => filter.Id == item.JobId)),
                        CreatedDate = item.CreatedDate,
                    };
                    jobBidDtos.Add(bidDto);
                }
                var dtos = _mapper.Map<List<JobBidDto>>(jobBidDtos);
                BidList.DataList = dtos;
            }
            return BidList;

        }

        public JobBidDto GetBidDetail(Guid jobId, Guid bidId)
        {

            var BidList = new ResultModel();
            var jobList = _jobRepository.Get();
            var job = jobList.FirstOrDefault(x => x.Id == jobId && x.JobStatusId != enums.JobEnums.JobStatus.JobInProgress.GetEnumValue());
            var Bid = _jobBidRepository.Get();
            var bid = Bid.FirstOrDefault(x => x.Id == bidId && x.BidStatus != JobBidStatus.BidSelected.GetEnumValue());
            var jobBidsAttachment = _jobBidAttachmentRepository.Get();
            var list = jobBidsAttachment.Where(x => x.JobBidId == bid.Id).ToList();
            bid.JobBidAttachments = list;
            var bidView = _mapper.Map<JobBidDto>(bid);
            bidView.Job = _mapper.Map<JobDto>(job);
            return bidView;
        }

        public ResultModel<JobDto> GetFreelancerJobList(int pageNo, int pageSize, int jobStatus)
        {
            var jobList = new ResultModel<JobDto>();
            var loggedInUserId = _httpContextAccessor.HttpContext.User?.GetUserId();
            var contractList = _jobContractRepository.Get();
            var data = _jobRepository.Get();
            if (!Guid.TryParse(loggedInUserId, out var userId))
            {
                jobList.Error = true;
                jobList.Message = JobBidConstants.BidSubmissionFailed;
                return jobList;
            }
            var Contract = contractList.Where(x => x.FreelancerId == userId).ToList();
            var response = _jobRepository.Get(x => Contract.Select(col => col.JobId).Contains(x.Id) && x.JobStatusId == jobStatus).ToList();
            jobList.TotalCount = response.Count;
            if (pageNo != 0)
            {
                response = response.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                var dtos = _mapper.Map<List<JobDto>>(response);
                jobList.DataList = dtos;
            }
            return jobList;
        }

        public JobDto FreelancerJobscount()
        {
            var loggedInUserId = _httpContextAccessor.HttpContext.User?.GetUserId();
            if (!Guid.TryParse(loggedInUserId, out var userId))
            {

            }
            var contractList = _jobContractRepository.Get();
            var Contract = contractList.Where(x => x.FreelancerId == userId).ToList();
            var data = _jobRepository.Get().ToList();

            var InprogresJobcount = data.Where(x => Contract.Select(col => col.JobId).Contains(x.Id) && x.JobStatusId == enums.JobEnums.JobStatus.JobInProgress.GetEnumValue()).ToList();

            var CompletedJobcount = data.Where(x => Contract.Select(col => col.JobId).Contains(x.Id) && x.JobStatusId == enums.JobEnums.JobStatus.JobCompleted.GetEnumValue()).ToList();

            var job = new JobDto()
            {
                InprogressJobCount = InprogresJobcount.Count(),
                CompletedJobCount = CompletedJobcount.Count()
            };
            return job;
        }

        public ResultModel UpdateMilestone(MilestoneDto milestoneDto)
        {
            var result = new ResultModel<MilestoneDto>();
            var userList = _usersRepository.Get();
            var loggedInUserId = _httpContextAccessor.HttpContext.User?.GetUserId();
            var user = userList.FirstOrDefault(x => x.Id == Guid.Parse(loggedInUserId));

            if (user.UserRole == UserRole.Freelancer)
            {
                if (_httpFileHandler != null)
                {
                    var httpFiles = _httpFileHandler.GetFromFiles();
                    if (httpFiles.Any())
                    {
                        _milestoneAttachmentHandler.ConvertFromWebFormToDto(milestoneDto, httpFiles);
                    }
                }
                _milestoneAttachmentHandler?.StoreAttachmentsToBlob(milestoneDto);
            }

            var milestone = _mapper.Map<Milestone>(milestoneDto);

            if (milestoneDto.MilestoneAttachments != null)
            {
                var milestoneAttachments = new List<MilestoneAttachment>();
                foreach (var attachments in milestoneDto.MilestoneAttachments)
                {
                    attachments.MilestoneId = milestone.Id;
                    milestoneAttachments.Add(_mapper.Map<MilestoneAttachment>(attachments));
                }
                _milestoneAttachmentRepository.AddList(milestoneAttachments);
                result.TotalCount = milestoneDto.MilestoneAttachments.Count();
            }

            if (user.UserRole == UserRole.Client)
            {

                if (milestoneDto.MilestoneStatus == Enums.MilestoneStatus.AddFunds.GetEnumValue())
                {
                    var contractFund = _mapper.Map<ContractMilestoneFund>(milestoneDto);
                    contractFund.FundStatus = FundStatus.Completed.GetEnumValue();
                    _contractFundRepository.Add(contractFund);
                    milestoneDto.MilestoneStatus = Enums.MilestoneStatus.Active.GetEnumValue();
                    result.Message = MessageConstants.FundAddedSuccessful;
                }

                else if (milestoneDto.MilestoneStatus == Enums.MilestoneStatus.MilestoneCompletedWaitingForClientReview.GetEnumValue())
                {
                    milestoneDto.MilestoneStatus = Enums.MilestoneStatus.ClientApproved.GetEnumValue();
                    result.Message = MessageConstants.MilestoneApproved;
                }

                else if (milestoneDto.MilestoneStatus == Enums.MilestoneStatus.ClientCancelledPendingRefund.GetEnumValue())
                {
                    milestoneDto.MilestoneStatus = Enums.MilestoneStatus.ClientCancelledPendingRefund.GetEnumValue();
                    result.Message = MessageConstants.MilestoneClose;
                }

            }
            else
            {
                if (milestoneDto.MilestoneStatus == Enums.MilestoneStatus.Active.GetEnumValue())
                {
                    milestoneDto.MilestoneStatus = Enums.MilestoneStatus.MilestoneCompletedWaitingForClientReview.GetEnumValue();
                    result.Message = MessageConstants.MilestoneApprove;
                }
                else if (milestoneDto.MilestoneStatus == Enums.MilestoneStatus.AdminApprovedFundsPaidToFreelancerWallet.GetEnumValue())
                {
                    milestoneDto.MilestoneStatus = Enums.MilestoneStatus.FreelancerWithdrawal.GetEnumValue();
                    result.Message = MessageConstants.MilestoneApprove;
                }
            }

            _milestoneRepository.Update(_mapper.Map<Milestone>(milestoneDto));

            result.Data = milestoneDto;
            result.Error = false;
            return result;
        }

        public MilestoneAttachmentDto GetMilestoneAttachment(Guid id)
        {
            var entity = _milestoneAttachmentRepository.Get(id);
            var attachment = _mapper.Map<MilestoneAttachmentDto>(entity);
            return attachment;
        }

        public ResultModel CloseJob(Guid jobId)
        {
            var result = new ResultModel();
            var jobList = _jobRepository.Get();
            var job = jobList.FirstOrDefault(x => x.Id == jobId);
            if (job != null)
            {
                job.JobStatusId = enums.JobEnums.JobStatus.JobCompleted.GetEnumValue();
                _jobRepository.Update(job);

                result.Error = false;
                result.Message = MessageConstants.JobCompleted;
            }
            else
            {
                result.Error = true;
                result.Message = MessageConstants.MilestonePending;
            }
            return result;
        }

        public ResultModel UpdateJobSatus(Guid jobid, int jobStatus)
        {
            var resultModel = new ResultModel();
            var loggedInUserId = _httpContextAccessor.HttpContext.User?.GetUserId();
            if (!Guid.TryParse(loggedInUserId, out var userId))
            {

            }
            var data = _jobRepository.Get();
            var jobDetails = data.FirstOrDefault(x => x.Id == jobid && x.UserId == userId && x.JobStatusId == enums.JobEnums.JobStatus.JobInProgress.GetEnumValue());
            if (jobDetails == null)
            {
                resultModel.Error = true;
                resultModel.Message = MessageConstants.ErrorMessageUnexpected;
            }
            else if (jobDetails != null)
            {
                jobDetails.JobStatusId = jobStatus;
                resultModel.Error = false;
                resultModel.Message = MessageConstants.JobUpdateStatusSuccessful;
                _jobRepository.Update(jobDetails);
            }

            return resultModel;
        }

        public async Task<ResultModel> SendEmailToClient(JobBidDto jobBidDto)
        {
            var res = new ResultModel();
            var userList = _usersRepository.Get();
            var jobList = _jobRepository.Get();
            var job = jobList.FirstOrDefault(x => x.Id == jobBidDto.JobId);
            var freelancer = userList.FirstOrDefault(x => x.Id == jobBidDto.FreelancerUserId);
            var client = userList.FirstOrDefault(x => x.Id == job.UserId);
            var model = new TemplateModel
            {
                ClientName = client.FirstName,
                FreelancerName = freelancer.FirstName,
                To = client.Email,
                JobTitle = job.JobTitle,
                PostDate = DateTime.UtcNow
            };
            var emailAudit = new EmailAudit
            {
                Content = "Need to hire Freelencers",
                SenderEmailAddress = freelancer.Email,
                ReceiverEmailAddress = client.Email,
                ReceiverId = client.Id,
                SenderId = freelancer.Id
            };
            _emailAuditRepository.Add(emailAudit);

            await _emailManager.SendEmailToClient(model);
            res.Error = false;
            res.Message = MessageConstants.EmailSentSuccessfully;
            return res;
        }

        public async Task<ResultModel> SendEmailToFreeLancer(Guid freelancerId)
        {
            var res = new ResultModel();
            var userList = _usersRepository.Get();

            var userId = _httpContextAccessor.HttpContext.User?.GetUserId();

            if (!Guid.TryParse(userId, out var clientId))
            {
            }

            var freelancer = userList.FirstOrDefault(x => x.Id == freelancerId);
            var client = userList.FirstOrDefault(x => x.Id == clientId);
            var model = new TemplateModel
            {
                ClientName = client.FirstName,
                FreelancerName = freelancer.FirstName,
                To = freelancer.Email,
                PostDate = DateTime.UtcNow
            };
            var emailAudit = new EmailAudit
            {
                Content = "Bid Accepted",
                SenderEmailAddress = client.Email,
                ReceiverEmailAddress = freelancer.Email,
                ReceiverId = freelancer.Id,
                SenderId = client.Id
            };
            _emailAuditRepository.Add(emailAudit);
            Response response = await _emailManager.SendEmailToFreelancer(model);
            res.Error = false;
            res.Message = MessageConstants.EmailSentSuccessfully;
            return res;
        }

        public async Task<ResultModel> SendEmailToFreeLancerForNegotiation(Guid freelancerId, bool test)
        {
            var res = new ResultModel();
            var model = new TemplateModel();
            var emailAudit = new EmailAudit();
            var userList = new List<User>();
            var userId = _httpContextAccessor.HttpContext.User?.GetUserId();
            if (!Guid.TryParse(userId, out var clientId))
            {
            }
            if (test == true)
            {
                var users = _usersRepository.Get();
                userList = users.Where(x => x.Id == freelancerId || x.Id == clientId).ToList();
            }
            else
            {
                userList = _usersRepository.Get(x => x.Id == freelancerId || x.Id == clientId).ToList();
            }

            foreach (var user in userList)
            {
                if (user.UserRole == UserRole.Client)
                {
                    model.ClientName = user.FirstName;
                    emailAudit.SenderEmailAddress = user.Email;
                    emailAudit.SenderId = user.Id;
                }
                else
                {
                    model.FreelancerName = user.FirstName;
                    emailAudit.ReceiverEmailAddress = user.Email;
                    emailAudit.ReceiverId = user.Id;
                    model.To = user.Email;
                }
            }
            model.PostDate = DateTime.UtcNow;
            emailAudit.Content = "Request to freelancer for amend their bid";

            _emailAuditRepository.Add(emailAudit);
            Response response = await _emailManager.SendEmailToFreeLancerForNegotiation(model);
            res.Error = false;
            res.Message = MessageConstants.EmailSentSuccessfully;

            return res;
        }
        public async Task<ResultModel> CompleteMilestoneEmailNotification(MilestoneDto milestoneDto)
        {
            var res = new ResultModel();

            var userList = _usersRepository.Get();

            var userId = _httpContextAccessor.HttpContext.User?.GetUserId();

            if (!Guid.TryParse(userId, out var freelancerId))
            {
            }

            var freelancer = userList.FirstOrDefault(x => x.Id == freelancerId);
            var client = userList.FirstOrDefault(x => x.Id == milestoneDto.CreatedId);
            var model = new TemplateModel
            {
                ClientName = client.FirstName,
                FreelancerName = freelancer.FirstName,
                To = client.Email,
                JobTitle = milestoneDto.Description,
                PostDate = DateTime.UtcNow
            };
            var emailAudit = new EmailAudit
            {
                Content = "Milestone completed by Freelancer",
                SenderEmailAddress = freelancer.Email,
                ReceiverEmailAddress = client.Email,
                ReceiverId = client.Id,
                SenderId = freelancer.Id
            };
            _emailAuditRepository.Add(emailAudit);
            Response response = await _emailManager.CompleteMilestoneEmailNotification(model);
            res.Error = false;
            res.Message = MessageConstants.EmailSentSuccessfully;
            return res;
        }


        public async Task<ResultModel> CompleteJobNotification(Guid contractId, bool test)
        {
            var result = new ResultModel();
            var model = new TemplateModel();
            var emailAudit = new EmailAudit();
            var jobContract = new JobContract();
            var job = new Job();
            var userList = new List<User>();

            if (test == true)
            {
                var users = _usersRepository.Get();
                var jobList = _jobRepository.Get();
                var jobContractList = _jobContractRepository.Get();

                jobContract = jobContractList.FirstOrDefault(x => x.Id == contractId);
                job = jobList.FirstOrDefault(x => x.Id == jobContract.JobId);

                userList = users.Where(x => x.Id == job.UserId || x.Id == jobContract.FreelancerId).ToList();
            }
            else
            {
                jobContract = _jobContractRepository.Get(x => x.Id == contractId).FirstOrDefault();
                job = _jobRepository.Get(x => x.Id == jobContract.JobId).FirstOrDefault();
                userList = _usersRepository.Get(x => x.Id == job.UserId || x.Id == jobContract.FreelancerId).ToList();
            }

            foreach (var user in userList)
            {
                if (user.UserRole == UserRole.Client)
                {
                    model.ClientName = user.FirstName;
                    emailAudit.ReceiverEmailAddress = user.Email;
                    emailAudit.ReceiverId = user.Id;
                    model.To = user.Email;
                }
                else
                {
                    model.FreelancerName = user.FirstName;
                    emailAudit.SenderEmailAddress = user.Email;
                    emailAudit.SenderId = user.Id;
                }
            }
            model.PostDate = DateTime.UtcNow;
            model.JobTitle = job.JobTitle;
            emailAudit.Content = "Request for complete job";

            _emailAuditRepository.Add(emailAudit);
            Response response = await _emailManager.CompleteJobNotification(model);
            result.Message = MessageConstants.EmailSentSuccessfully;
            result.Error = false;
            return result;
        }

        public ResultModel GetActiveMilestoneCount(Guid jobContractId, bool test)
        {
            var result = new ResultModel();
            var activeMilestone = new List<Milestone>();
            if (test == true)
            {
                var milestoneList = _milestoneRepository.Get();
                activeMilestone = milestoneList.Where(x => x.ContractId == jobContractId && (x.MilestoneStatus == Enums.MilestoneStatus.Active.GetEnumValue() || x.MilestoneStatus == Enums.MilestoneStatus.AddFunds.GetEnumValue())).ToList();
            }
            else
            {
                activeMilestone = _milestoneRepository.Get(x => x.ContractId == jobContractId && (x.MilestoneStatus == Enums.MilestoneStatus.Active.GetEnumValue() || x.MilestoneStatus == Enums.MilestoneStatus.AddFunds.GetEnumValue())).ToList();
            }
            result.TotalCount = activeMilestone.Count();
            result.Error = false;
            return result;
        }

        public bool HasAlreadyBid(Guid id)
        {
            var userId = _httpContextAccessor.HttpContext.User?.GetUserId();
            if (!Guid.TryParse(userId, out var freelancerId))
            {
            }
            var jobBid = _jobBidRepository.Get(x => x.JobId == id && x.FreelancerUserId == freelancerId).Count();
            if (jobBid == 0)
            {
                return false;
            }
            else
                return true;
        }
    }
}
