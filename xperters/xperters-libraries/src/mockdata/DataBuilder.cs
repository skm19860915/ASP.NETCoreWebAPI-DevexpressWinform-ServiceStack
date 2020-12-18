using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using xperters.domain;
using xperters.entities;
using xperters.entities.Entities;
using xperters.entities.Extensions;
using xperters.fileutilities.Interfaces;

namespace xperters.mockdata
{
    public class DataBuilder
    {
        private readonly Assembly _assembly;
        public List<JobDto> MockJobDtos { get; private set; }
        private List<JobBidDto> _mockJobBidDtos;
        private int _numberItemsSaved;
        private readonly ILogger _logger;
        private readonly XpertersContext _context;
        private readonly IMapper _mapper;
        public List<MilestoneDto> Milestones { get; private set; }
        public DataBuilder(XpertersContext context, IMapper mapper, ILogger logger)
        {
            _context = context;

            _mapper = mapper ?? throw new ArgumentNullException("Mapper has not be set");
            _logger = logger ?? throw new ArgumentNullException("Logger has not be set");

            _assembly = Assembly.GetExecutingAssembly();
            MockDataFactory.Create();
        }

        public void ApplyMockData(IAttachmentHandler<JobDto> attachmentHandler                                        
                                    , IAttachmentHandler<JobBidDto> bidAttachmentHandler
                                    , IAttachmentHandler<MilestoneDto> milestoneAttachmentHandler)
        {

            // drop data from these tables.
            RemoveAll();
            
            // Add files data for attachments 
            InitializeFileDataForMocks();

            // Users need to be instantiated first
            AddUsers();

            AddRequestPayerStatus();

            //Jobs
            AddJobs(attachmentHandler);

            AddJobStatus();

            // Then Bids
            AddJobBids(bidAttachmentHandler);

            // Then JobBidChatSession
            AddJobBidChatSessions();

            //JobBidChatSessionUser
            AddJobBidChatSessionUsers();

            //JobBidChatMessages
            AddJobBidChatMessages();

            //ContractChatSession
            AddContractChatSessions();

            //ContractChatSessionUser
            AddContractChatSessionUsers();

            //ContractChatMessage
            AddContractChatMessages();

            //JobContracts
            AddJobContracts();

            //Milestones
            AddMilestones();

            AddMilestoneRequestPayers();

            AddMilestoneSystemRequestPayers();

            AddMilestoneAttachments(milestoneAttachmentHandler);
          
            //Add EmailAudit
            AddEmailAudit();

            //Add accountDetails
            AddAccountDetails();
            AddFeeStructures();
            //Add Cards
            AddCards(); 
            
            // User payments data
            AddUserPayments();
            AddUserBalances();
            AddUserWithdrawals();

            // User payments data
            AddSystemPayments();
            AddSystemBalances();
        }

        private void AddUserPayments()
        {
            var userPayments = UserPaymentsMock.Get();

            foreach (var dto in userPayments)
            {
                var bFoundPayment = _context.UserPayments.Find(dto.Id);

                if (bFoundPayment == null)
                {
                    var payment = new UserPayment
                    {
                        Id = dto.Id,
                        FromUserId = dto.FromUserId,
                        ToUserId = dto.ToUserId,
                        MilestoneRequestPayerId = dto.MilestoneRequestPayerId,
                        MilestoneSystemRequestPayerId = dto.MilestoneSystemRequestPayerId,
                        PaymentTransactionTypeId = dto.PaymentTransactionTypeId,
                        CurrencyId = dto.CurrencyId,
                        Amount = dto.Amount,
                        Balance = dto.Balance,
                    };

                    _context.UserPayments.Add(payment);
                    _numberItemsSaved = _context.SaveChanges();
                }
            }

            _logger.LogDebug($"Number of payments saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.UserPayments.Count();
            _logger.LogDebug($"Number of payments available: {_numberItemsSaved}");
        }        
        
        
        private void AddSystemPayments()
        {
            var systemPayments = SystemPayments.Get();

            foreach (var dto in systemPayments)
            {
                var payment = _mapper.Map<SystemPayment>(dto);
                var bFoundPayment = _context.SystemPayments.Find(payment.Id);

                if (bFoundPayment == null)
                {
                    _context.SystemPayments.Add(payment);
                }
            }

            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of payments saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.SystemPayments.Count();
            _logger.LogDebug($"Number of payments available: {_numberItemsSaved}");
        }

        private void AddUserWithdrawals()
        {
            var withdrawals = UserWithdrawals.Get();

            foreach (var dto in withdrawals)
            {
                var withdrawal = _mapper.Map<UserWithdrawal>(dto);
                var foundWithdrawal = _context.UserWithdrawals.Find(withdrawal.Id);

                if (foundWithdrawal == null)
                {
                    _context.UserWithdrawals.Add(withdrawal);
                }
            }

            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of withdrawals saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.UserWithdrawals.Count();
            _logger.LogDebug($"Number of withdrawals available: {_numberItemsSaved}");
        }

        private void AddUserBalances()
        {
            var userBalances = UserBalancesMock.Get();

            foreach (var dto in userBalances)
            {
                var balance = _mapper.Map<UserBalance>(dto);
                var bFoundBalance = _context.UserBalances.Find(balance.Id);

                if (bFoundBalance == null)
                {
                    _context.UserBalances.Add(balance);
                }
            }

            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of balances saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.UserBalances.Count();
            _logger.LogDebug($"Number of balances available: {_numberItemsSaved}");
        }
        
        private void AddSystemBalances()
        {
            var systemBalances = SystemBalancesMock.Get();

            foreach (var dto in systemBalances)
            {
                var balance = _mapper.Map<SystemBalance>(dto);
                var bFoundBalance = _context.SystemBalances.Find(balance.Id);

                if (bFoundBalance == null)
                {
                    _context.SystemBalances.Add(balance);
                }
            }

            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of balances saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.SystemBalances.Count();
            _logger.LogDebug($"Number of balances available: {_numberItemsSaved}");
        }

        public void InitializeFileDataForMocks()
        {
            var jobDtos = Jobs.Get();
            var jobBidDtos = JobBids.Get();

            var attachmentDtos = JobAttachments.Get();
            var bidAttachmentsDto = JobBidAttachments.Get();

            var milestoneDtos = mockdata.Milestones.Get();
            var milestoneAttachmentDtos = MilestoneAttachmentMock.Get();

            // add the attachments data from files
            foreach (var attachment in attachmentDtos)
            {
                AddAttachment(attachment, $"xperters.mockdata.TestFiles.{attachment.Uri}");
                var job = jobDtos.First(f => f.Id == attachment.JobId);
                job.JobAttachments.Add(attachment);
            }

            //Add BidAttachments to bid Table
            foreach (var attachment in bidAttachmentsDto)
            {
                AddBidAttachment(attachment, $"xperters.mockdata.TestFiles.{attachment.Uri}");
                var bid = jobBidDtos.First(f => f.Id == attachment.JobBidId);
                var bidAttachment = bid.JobBidAttachments.First(x => x.Id == attachment.Id);
                if (bidAttachment == null)
                {
                    bid.JobBidAttachments.Add(attachment);
                }
            }

            //Add MilestoneAttachments to Milestone 
            foreach (var attachment in milestoneAttachmentDtos)
            {
                AddMilestoneAttachment(attachment, $"xperters.mockdata.TestFiles.{attachment.Uri}");
                var milestone = milestoneDtos.First(f => f.Id == attachment.MilestoneId);
                milestone.MilestoneAttachments.Add(attachment);
            }


            MockJobDtos = jobDtos;
            _mockJobBidDtos = jobBidDtos;
            Milestones = milestoneDtos;
        }

        private  void AddMilestoneAttachment(MilestoneAttachmentDto attachmentDto, string attachmentsResourceId)
        {
            using (var stream = _assembly.GetManifestResourceStream(attachmentsResourceId))
            {
                if (stream != null)
                {
                    attachmentDto.FileData = new byte[stream.Length];
                    stream.Read(attachmentDto.FileData, 0, attachmentDto.FileData.Length);
                }
            }
        }

        private void AddAttachment(JobAttachmentDto attachmentDto, string attachmentsResourceId)
        {
            using (var stream = _assembly.GetManifestResourceStream(attachmentsResourceId))
            {
                if (stream != null)
                {
                    attachmentDto.FileData = new byte[stream.Length];
                    stream.Read(attachmentDto.FileData, 0, attachmentDto.FileData.Length);
                }
            }
        }

        
        private void AddBidAttachment(JobBidAttachmentDto attachmentDto, string attachmentsResourceId)
        {
            using (var stream = _assembly.GetManifestResourceStream(attachmentsResourceId))
            {
                if (stream != null)
                {
                    attachmentDto.FileData = new byte[stream.Length];
                    stream.Read(attachmentDto.FileData, 0, attachmentDto.FileData.Length);
                }
            }
        }

        private void AddJobBids(IAttachmentHandler<JobBidDto> bidAtachmentHandler)
        {
            foreach (var bid in _mockJobBidDtos)
            {
                bidAtachmentHandler.StoreAttachmentsToBlob(bid);
                var bidList = _mapper.Map<JobBid>(bid);
                var bFoundBid = _context.JobBids.Find(bidList.Id);
                // var bFoundBid = _context.JobBids.Where(x=>x.Id == bidList.Id).FirstOrDefault();
                if (bFoundBid == null)
                {
                    bidList.FreelancerUser = null;                 
                    _context.JobBids.Add(bidList);
                }
            }

            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of Bids saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.Jobs.Count();
            _logger.LogDebug($"Number of Bids available: {_numberItemsSaved}");
        }

       
        private void AddJobs(IAttachmentHandler<JobDto> attachmentHandler)
        {
            foreach (var dto in MockJobDtos)
            {
                attachmentHandler.StoreAttachmentsToBlob(dto);
                var job = _mapper.Map<Job>(dto);
                var bFoundJob = _context.Jobs.Find(job.Id);

                if (bFoundJob == null)
                {
                    job.User = null;
                    _context.Jobs.Add(job);
                }
            }

            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of job saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.Jobs.Count();
            _logger.LogDebug($"Number of jobs available: {_numberItemsSaved}");
        }

        private void AddJobStatus()
        {
            var jobStatuses = JobStatusMock.Get();
            var items = _mapper.Map<IEnumerable<JobStatus>>(jobStatuses);

            foreach (var jobStatus in items)
            {
                var found = _context.JobStatus.Find(jobStatus.JobStatusId);

                if (found == null)
                {
                    _context.JobStatus.Add(jobStatus);
                }
            }

            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of jobstatus saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.JobStatus.Count();
            _logger.LogDebug($"Number of jobstatus available: {_numberItemsSaved}");
        }

        private void AddRequestPayerStatus()
        {
            var list = RequestPayerStatusMock.Get();
            var items = _mapper.Map<IEnumerable<RequestPayerStatus>>(list);

            foreach(var item in items)
            {
                var found = _context.RequestPayerStatus.Find(item.PayerStatusId);

                if (found == null)
                {
                    _context.RequestPayerStatus.Add(item);
                }
            }

            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of RequestPayerStatus saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.RequestPayerStatus.Count();
            _logger.LogDebug($"Number of RequestPayerStatus available: {_numberItemsSaved}");
        }
       

        private void AddUsers()
        {
            var applicationUsers = Users.Get();
            var users = _mapper.Map<IEnumerable<User>>(applicationUsers);
            foreach (var user in users)
            {
                user.AddUserToStore(_context, _logger);
            }

            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of users saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.Users.Count();
            _logger.LogDebug($"Number of users available: {_numberItemsSaved}");
        }

        private void AddJobBidChatSessions()
        {

            var mockJobBidSession = JobBidSessionMock.Get();
            foreach (var jobBidSession in mockJobBidSession)
            {
                var sessionList = _mapper.Map<JobBidChatSession>(jobBidSession);

                sessionList.Client = null;
                sessionList.Freelancer= null;
                var foundSession = _context.JobBidChatSessions.Find(sessionList.Id);

                if (foundSession == null)
                {
                    _context.JobBidChatSessions.Add(sessionList);
                }
            }
            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of JobBidChatSession saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.JobBidChatSessions.Count();
            _logger.LogDebug($"Number of JobBidChatSession available: {_numberItemsSaved}");
        }

        private void AddJobBidChatSessionUsers()
        {
            var mockJobBidSessionUsers = JobBidChatSessionUsersMock.Get();
            foreach (var jobBidSessionUser in mockJobBidSessionUsers)
            {
                var sessionUserList = _mapper.Map<JobBidChatSessionUser>(jobBidSessionUser);
                var foundSessionUser = _context.JobBidChatSessionUsers.Find(sessionUserList.Id);
                sessionUserList.User = null;
                sessionUserList.JobBidChatSession = null;

                if (foundSessionUser == null)
                {
                    _context.JobBidChatSessionUsers.Add(sessionUserList);
                }
            }
            _numberItemsSaved = _context.SaveChanges();

            _logger.LogDebug($"Number of JobBidChatSessionUser saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.JobBidChatSessionUsers.Count();
            _logger.LogDebug($"Number of JobBidChatSessionUser available: {_numberItemsSaved}");

        }

        private void AddJobBidChatMessages()
        {
            
            var jobBidMessages = JobBidChatMessagesMock.Get();

            foreach (var message in jobBidMessages)
            {
                var messages = _mapper.Map<JobBidChatMessage>(message);
                messages.JobBidChatSession = null;
                messages.Sender = null;

                var foundMessage = _context.JobBidChatMessages.Find(messages.Id);
                if (foundMessage == null)
                {
                    _context.JobBidChatMessages.Add(messages);
                }
            }

            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of JobBidChatMessages saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.JobBidChatMessages.Count();
            _logger.LogDebug($"Number of JobBidChatMessages available: {_numberItemsSaved}");
        }

        private void AddContractChatSessions()
        {
            var mockContractSession = ContractChatSessionsMock.Get();
            foreach (var session in mockContractSession)
            {
                var sessionList = _mapper.Map<ContractChatSession>(session);
                var bFoundSession = _context.ContractChatSessions.Find(sessionList.Id);
                if (bFoundSession == null)
                {
                    _context.ContractChatSessions.Add(sessionList);
                }
            }
            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of ContractSessions saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.ContractChatSessions.Count();
            _logger.LogDebug($"Number of ContractSessions available: {_numberItemsSaved}");
        }

        private void AddContractChatSessionUsers()
        {
            var mockContractSessionUser = ContractChatSessionUsersMock.Get();
            foreach (var contractUser in mockContractSessionUser)
            {
                var sessionUserList = _mapper.Map<ContractChatSessionUser>(contractUser);
                var bFoundSessionUser = _context.ContractChatSessionUsers.Find(sessionUserList.Id);
                if (bFoundSessionUser == null)
                {
                    _context.ContractChatSessionUsers.Add(sessionUserList);
                }
            }
            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of ContractChatSessionUser saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.ContractChatSessionUsers.Count();
            _logger.LogDebug($"Number of ContractChatSessionUser available: {_numberItemsSaved}");
        }

        private void AddContractChatMessages()
        {
            var mockContractMesssage = ContractChatMessagesMock.Get();
            foreach (var message in mockContractMesssage)
            {
                var messages = _mapper.Map<ContractChatMessage>(message);
                var foundMessage = _context.ContractChatMessages.Find(messages.Id);
                if (foundMessage == null)
                {
                    _context.ContractChatMessages.Add(messages);
                }
            }
            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of ContractChatMessage saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.ContractChatMessages.Count();
            _logger.LogDebug($"Number of ContractChatMessage available: {_numberItemsSaved}");
        }

        private void AddMilestones()
        {
            foreach (var dto in Milestones)
            {
                var milestone = _mapper.Map<Milestone>(dto);
                milestone.MilestoneAttachments = null;
                var foundMilestone = _context.Milestones.Find(milestone.Id);

                if (foundMilestone == null)
                {
                    milestone.Contract.Job = null;
                    milestone.Contract = null;
                    milestone.MilestoneRequestPayers = null;
                    _context.Milestones.Add(milestone);
                }
            }

            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of milestone saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.Milestones.Count();
            _logger.LogDebug($"Number of milestone available: {_numberItemsSaved}");
        }

        private void AddMilestoneRequestPayers()
        {
            var milestoneRequestPayerDtos = MilestoneRequestPayers.Get();

            foreach (var dto in milestoneRequestPayerDtos)
            {
                var milestoneRequestPayer = _mapper.Map<MilestoneRequestPayer>(dto);

                var found = _context.MilestoneRequestPayers.Find(milestoneRequestPayer.Id);

                if (found == null)
                {
                    milestoneRequestPayer.PayerStatus = null;
                    milestoneRequestPayer.Milestone = null;
                    _context.MilestoneRequestPayers.Add(milestoneRequestPayer);
                }
            }

            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of milestoneRequestPayer saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.Milestones.Count();
            _logger.LogDebug($"Number of milestoneRequestPayer available: {_numberItemsSaved}");
        }        
        
        private void AddMilestoneSystemRequestPayers()
        {
            var milestoneSystemRequests = MilestoneSystemRequestPayers.Get();

            foreach (var dto in milestoneSystemRequests)
            {
                var milestoneSystemRequestPayer = _mapper.Map<MilestoneSystemRequestPayer>(dto);

                var found = _context.MilestoneSystemRequestPayers.Find(milestoneSystemRequestPayer.Id);

                if (found == null)
                {
                    milestoneSystemRequestPayer.PayerStatus = null;
                    _context.MilestoneSystemRequestPayers.Add(milestoneSystemRequestPayer);
                }
            }

            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of milestoneSystemRequestPayer saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.Milestones.Count();
            _logger.LogDebug($"Number of milestoneSystemRequestPayer available: {_numberItemsSaved}");
        }

        private void AddMilestoneAttachments(IAttachmentHandler<MilestoneDto> attachmentHandler)
        {
            foreach (var dto in Milestones)
            {
                attachmentHandler.StoreAttachmentsToBlob(dto);
                var attachments = _mapper.Map<List<MilestoneAttachment>>(dto.MilestoneAttachments);

                if (attachments == null) continue;

                foreach (var attachment in attachments)
                {
                    var foundAttachment = _context.MilestoneAttachments.Find(attachment.Id);

                    if (foundAttachment == null)
                    {
                        _context.MilestoneAttachments.Add(attachment);
                    }

                }
            }

            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of milestone saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.Milestones.Count();
            _logger.LogDebug($"Number of milestone available: {_numberItemsSaved}");
        }

        private void AddJobContracts()
        {
            var jobContractList = JobContracts.Get();
            foreach (var item in jobContractList)
            {
                var contract = _mapper.Map<JobContract>(item);
                var bFoundContract = _context.JobContracts.Find(contract.Id);

                if (bFoundContract == null)
                {
                    contract.Job = null;
                    contract.Freelancer = null;
                    contract.Milestones = null;
                    _context.JobContracts.Add(contract);
                }
            }

           _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of contracts saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.JobContracts.Count();
            _logger.LogDebug($"Number of contracts available: {_numberItemsSaved}");
        }
        
        private void AddEmailAudit()
        {
            var mockEmailAudit = EmailAuditMock.Get();
            foreach (var email in mockEmailAudit)
            {
                var emaildata = _mapper.Map<EmailAudit>(email);
                var foundMessage = _context.Cards.Find(emaildata.Id);
                if (foundMessage == null)
                {
                    _context.EmailAudits.Add(emaildata);
                }
            }
            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of card saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.EmailAudits.Count();
            _logger.LogDebug($"Number of card available: {_numberItemsSaved}");
        }

        private void AddAccountDetails()
        {
            var mockAccountDetail = AccountDetailMock.Get();
            foreach (var detail in mockAccountDetail)
            {
                var accountDetail = _mapper.Map<AccountDetail>(detail);

                accountDetail.User = null;

                var foundMessage = _context.AccountDetails.Find(detail.Id);
                if (foundMessage == null)
                {
                    _context.AccountDetails.Add(accountDetail);
                }
            }
            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of accountDetail saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.AccountDetails.Count();
            _logger.LogDebug($"Number of accountDetail available: {_numberItemsSaved}");
        }
        
        private void AddCards()
        {
            var cards = CardsMock.Get();

            foreach (var card in cards)
            {
                var card1 = _mapper.Map<Card>(card);
                var found = _context.Cards.FirstOrDefault(s => s.Id == card.Id);
                if (found == null && _context.Entry(card1).State != EntityState.Added)
                {
                    //To remove Id conflict, set the user model to null
                    card1.User = null;
                    _context.Cards.Add(card1);
                }
            }

            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of card saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.Cards.Count();
            _logger.LogDebug($"Number of card available: {_numberItemsSaved}");
        }        
        
        private void AddFeeStructures()
        {
            var feeStructures = MasterDataFactory.GetFeeStructureData();

            foreach (var feeStructure in feeStructures)
            {
                var found = _context.FeeStructures.FirstOrDefault(s => s.Id == feeStructure.Id);
                if (found == null && _context.Entry(feeStructure).State != EntityState.Added)
                {
                    //To remove Id conflict, set the user model to null
                    _context.FeeStructures.Add(feeStructure);
                }
            }

            _numberItemsSaved = _context.SaveChanges();
            _logger.LogDebug($"Number of fee structures saved: {_numberItemsSaved}");
            _numberItemsSaved = _context.Cards.Count();
            _logger.LogDebug($"Number of fee structures available: {_numberItemsSaved}");
        }


        private void RemoveAll()
        {
            if(_context.Database.IsSqlServer()){
                _context.Database.ExecuteSqlRaw("DELETE FROM [ContractChatMessages]");
                _context.Database.ExecuteSqlRaw("DELETE FROM [ContractChatSessionUsers]");
                _context.Database.ExecuteSqlRaw("DELETE FROM [ContractChatSessions]");
                _context.Database.ExecuteSqlRaw("DELETE FROM [ContractMilestoneFunds]");
                _context.Database.ExecuteSqlRaw("DELETE FROM [UserBalances]");
                _context.Database.ExecuteSqlRaw("DELETE FROM [UserPayments]");
                _context.Database.ExecuteSqlRaw("DELETE FROM [UserWithdrawals]");
                _context.Database.ExecuteSqlRaw("IF EXISTS (SELECT 0 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'FeeStructures') BEGIN DELETE FROM dbo.[FeeStructures];END ");
                _context.Database.ExecuteSqlRaw("DELETE FROM [Milestones]");
                _context.Database.ExecuteSqlRaw("DELETE FROM [JobBids]");
                _context.Database.ExecuteSqlRaw("DELETE FROM [JobContracts]");
                _context.Database.ExecuteSqlRaw("DELETE FROM [Cards]");
                _context.Database.ExecuteSqlRaw("DELETE FROM [EmailAudits]");
                _context.Database.ExecuteSqlRaw("DELETE FROM [AccountDetails]");
                _context.Database.ExecuteSqlRaw("DELETE FROM [Jobs]");
                _context.Database.ExecuteSqlRaw("DELETE FROM [Users]");
            }
            else
            {
                _context.ContractChatMessages.RemoveRange(_context.ContractChatMessages);
                _context.ContractChatSessionUsers.RemoveRange(_context.ContractChatSessionUsers);
                _context.ContractChatSessions.RemoveRange(_context.ContractChatSessions);
                _context.ContractMilestoneFunds.RemoveRange(_context.ContractMilestoneFunds);
                _context.UserBalances.RemoveRange(_context.UserBalances);
                _context.UserPayments.RemoveRange(_context.UserPayments);
                _context.UserWithdrawals.RemoveRange(_context.UserWithdrawals);
                _context.FeeStructures.RemoveRange(_context.FeeStructures);
                _context.Milestones.RemoveRange(_context.Milestones);
                _context.JobBids.RemoveRange(_context.JobBids);
                _context.JobContracts.RemoveRange(_context.JobContracts);
                _context.Cards.RemoveRange(_context.Cards);
                _context.EmailAudits.RemoveRange(_context.EmailAudits);
                _context.AccountDetails.RemoveRange(_context.AccountDetails);
                _context.Jobs.RemoveRange(_context.Jobs);
                _context.Users.RemoveRange(_context.Users);
            }

            _numberItemsSaved = _context.SaveChanges();
            _context.ChangeTracker.AcceptAllChanges();

            _logger.LogDebug($"Number of items removed: {_numberItemsSaved}");

        }
    }
}
