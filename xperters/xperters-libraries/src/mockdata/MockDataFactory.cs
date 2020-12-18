using System.Collections.Generic;
using System.Linq;
using xperters.domain;

namespace xperters.mockdata
{
    /// <summary>
    /// Starts the process of creating mock data and all expected relationships
    /// </summary>
    public static class MockDataFactory
    {
        private static List<JobAttachmentDto> _jobAttachments;
        private static List<JobBidDto> _jobBids;
        private static List<JobDto> _jobs;
        private static IEnumerable<UserDto> _users;
        private static List<JobBidAttachmentDto> _jobBidAttachment;
        private static List<ContractChatSessionDto> _contractChatSessions;
        private static List<ContractChatSessionUserDto> _contractChatSessionUsers;
        private static List<ContractChatMessageDto> _contractChatMessages;
        private static List<JobBidChatSessionDto> _jobBidChatSession;
        private static List<JobBidChatSessionUsersDto> _jobBidChatSessionUsers;
        private static List<JobBidChatMessageDto> _jobBidChatMessages;
      
        private static List<MilestoneDto> _milestones;
        private static List<MilestoneAttachmentDto> _milestoneAttachments;
        private static List<EmailAttachmentsDto> _emailAttachments;
        private static List<EmailAuditDto> _emailAudits;
        private static List<AccountDetailDto> _accountDetails;
        private static List<CardDto> _cards;
        private static List<UserBalanceDto> _userBalances;
        private static List<UserPaymentDto> _userPayments;
        private static List<UserWithdrawalDto> _userWithdrawals;

        public static void Create()
        {
            _jobs = Jobs.Get();
            _jobBids = JobBids.Get();
            _users = Users.Get();
            _userBalances = UserBalancesMock.Get();
            _userPayments = UserPaymentsMock.Get();
            _userWithdrawals = UserWithdrawals.Get();

            _jobAttachments = JobAttachments.Get();
            _jobBidAttachment = JobBidAttachments.Get();
            _contractChatSessions = ContractChatSessionsMock.Get();
            _contractChatSessionUsers = ContractChatSessionUsersMock.Get();
            _contractChatMessages = ContractChatMessagesMock.Get();
            _jobBidChatSession = JobBidSessionMock.Get();
            _jobBidChatSessionUsers = JobBidChatSessionUsersMock.Get();
            _jobBidChatMessages = JobBidChatMessagesMock.Get();
            _milestones = Milestones.Get();
            _milestoneAttachments = MilestoneAttachmentMock.Get();
            _emailAttachments = EmailAttachmentsMock.Get();
            _emailAudits = EmailAuditMock.Get();
            _accountDetails = AccountDetailMock.Get();
            _cards = CardsMock.GetDtos();
            // test need to be build in the correct order
            // because they have dependencies
            BuildUsers();
            BuildJobStatus();
            BuildCurrencyType();
            BuildRequestPayerStatus();
            MilestoneRequestPayers();
            BuildMilestoneStatus();
            BuildJobAttachments();
            BuildJobBids();
            BuildJobContractList();
            BuildJobContract();
            BuildJobBidAttachments();
            BuildContractChatSessions();
            BuildContractChatSessionUser();
            BuildContractChatMessages();
            BuildJobBidChatSessions();
            BuildJobBidChatSessionUser();
            BuildJobBidChatMessages();
            BuildMilestoneAttachments();
            BuildEmailAttachments();
            BuildAccountDetails();
            BuildCards();
        }

        private static void BuildJobStatus()
        {
            JobStatusMock.JobStatusPosted.JobStatusId = JobStatusMock.JobStatusPosted.JobStatusId;
            JobStatusMock.JobStatusPosted.Status = JobStatusMock.JobStatusPosted.Status;
            JobStatusMock.JobStatusPosted.IsActive = JobStatusMock.JobStatusPosted.IsActive;

            JobStatusMock.JobStatusContractSigned.JobStatusId = JobStatusMock.JobStatusContractSigned.JobStatusId;
            JobStatusMock.JobStatusContractSigned.Status = JobStatusMock.JobStatusContractSigned.Status;
            JobStatusMock.JobStatusContractSigned.IsActive = JobStatusMock.JobStatusContractSigned.IsActive;

            JobStatusMock.JobStatusInProgress.JobStatusId = JobStatusMock.JobStatusInProgress.JobStatusId;
            JobStatusMock.JobStatusInProgress.Status = JobStatusMock.JobStatusInProgress.Status;
            JobStatusMock.JobStatusInProgress.IsActive = JobStatusMock.JobStatusInProgress.IsActive;

            JobStatusMock.JobStatusCompleted.JobStatusId = JobStatusMock.JobStatusCompleted.JobStatusId;
            JobStatusMock.JobStatusCompleted.Status = JobStatusMock.JobStatusCompleted.Status;
            JobStatusMock.JobStatusCompleted.IsActive = JobStatusMock.JobStatusCompleted.IsActive;

            JobStatusMock.JobStatusCanceled.JobStatusId = JobStatusMock.JobStatusCanceled.JobStatusId;
            JobStatusMock.JobStatusCanceled.Status = JobStatusMock.JobStatusCanceled.Status;
            JobStatusMock.JobStatusCanceled.IsActive = JobStatusMock.JobStatusCanceled.IsActive;
        }

        private static void BuildMilestoneStatus()
        {
            MilestoneStatusMock.MileStoneStatus1.MilestoneStatusId = MilestoneStatusMock.MileStoneStatus1.MilestoneStatusId;
            MilestoneStatusMock.MileStoneStatus1.StatusDescription = MilestoneStatusMock.MileStoneStatus1.StatusDescription;
            MilestoneStatusMock.MileStoneStatus1.IsActive = MilestoneStatusMock.MileStoneStatus1.IsActive;

            MilestoneStatusMock.MileStoneStatus2.MilestoneStatusId = MilestoneStatusMock.MileStoneStatus2.MilestoneStatusId;
            MilestoneStatusMock.MileStoneStatus2.StatusDescription = MilestoneStatusMock.MileStoneStatus2.StatusDescription;
            MilestoneStatusMock.MileStoneStatus2.IsActive = MilestoneStatusMock.MileStoneStatus2.IsActive;

            MilestoneStatusMock.MileStoneStatus3.MilestoneStatusId = MilestoneStatusMock.MileStoneStatus3.MilestoneStatusId;
            MilestoneStatusMock.MileStoneStatus3.StatusDescription = MilestoneStatusMock.MileStoneStatus3.StatusDescription;
            MilestoneStatusMock.MileStoneStatus3.IsActive = MilestoneStatusMock.MileStoneStatus3.IsActive;

            MilestoneStatusMock.MileStoneStatus4.MilestoneStatusId = MilestoneStatusMock.MileStoneStatus4.MilestoneStatusId;
            MilestoneStatusMock.MileStoneStatus4.StatusDescription = MilestoneStatusMock.MileStoneStatus4.StatusDescription;
            MilestoneStatusMock.MileStoneStatus4.IsActive = MilestoneStatusMock.MileStoneStatus4.IsActive;

            MilestoneStatusMock.MileStoneStatus5.MilestoneStatusId = MilestoneStatusMock.MileStoneStatus5.MilestoneStatusId;
            MilestoneStatusMock.MileStoneStatus5.StatusDescription = MilestoneStatusMock.MileStoneStatus5.StatusDescription;
            MilestoneStatusMock.MileStoneStatus5.IsActive = MilestoneStatusMock.MileStoneStatus5.IsActive;

            MilestoneStatusMock.MileStoneStatus6.MilestoneStatusId = MilestoneStatusMock.MileStoneStatus6.MilestoneStatusId;
            MilestoneStatusMock.MileStoneStatus6.StatusDescription = MilestoneStatusMock.MileStoneStatus6.StatusDescription;
            MilestoneStatusMock.MileStoneStatus6.IsActive = MilestoneStatusMock.MileStoneStatus6.IsActive;
        }

        private static void MilestoneRequestPayers()
        {
            Milestones.MilestoneA.MilestoneRequestPayers = mockdata.MilestoneRequestPayers.Get();
        }

        private static void BuildCurrencyType()
        {
            CurrencyMock.currencies1.CurrencyId = CurrencyMock.currencies1.CurrencyId;
            CurrencyMock.currencies1.CurrencyCode = CurrencyMock.currencies1.CurrencyCode;
        }

        private static void BuildRequestPayerStatus()
        {
            RequestPayerStatusMock.RequestPayerStatus1.PayerStatusId = RequestPayerStatusMock.RequestPayerStatus1.PayerStatusId;
            RequestPayerStatusMock.RequestPayerStatus1.PayerStatus = RequestPayerStatusMock.RequestPayerStatus1.PayerStatus;
            RequestPayerStatusMock.RequestPayerStatus1.IsActive = RequestPayerStatusMock.RequestPayerStatus1.IsActive;

            RequestPayerStatusMock.RequestPayerStatus2.PayerStatusId = RequestPayerStatusMock.RequestPayerStatus2.PayerStatusId;
            RequestPayerStatusMock.RequestPayerStatus2.PayerStatus = RequestPayerStatusMock.RequestPayerStatus2.PayerStatus;
            RequestPayerStatusMock.RequestPayerStatus2.IsActive = RequestPayerStatusMock.RequestPayerStatus2.IsActive;

            RequestPayerStatusMock.RequestPayerStatus3.PayerStatusId = RequestPayerStatusMock.RequestPayerStatus3.PayerStatusId;
            RequestPayerStatusMock.RequestPayerStatus3.PayerStatus = RequestPayerStatusMock.RequestPayerStatus3.PayerStatus;
            RequestPayerStatusMock.RequestPayerStatus3.IsActive = RequestPayerStatusMock.RequestPayerStatus3.IsActive;

            RequestPayerStatusMock.RequestPayerStatus4.PayerStatusId = RequestPayerStatusMock.RequestPayerStatus4.PayerStatusId;
            RequestPayerStatusMock.RequestPayerStatus4.PayerStatus = RequestPayerStatusMock.RequestPayerStatus4.PayerStatus;
            RequestPayerStatusMock.RequestPayerStatus4.IsActive = RequestPayerStatusMock.RequestPayerStatus4.IsActive;
        }

        private static void BuildJobContractList()
        {
            JobContracts.JobContract1.JobId = Jobs.Job11.Id;
            JobContracts.JobContract1.Job = Jobs.Job11;
            JobContracts.JobContract1.FreelancerId = Users.FreelancerFirst.Id;

            JobContracts.JobContract2.JobId = Jobs.Job12.Id;
            JobContracts.JobContract2.Job = Jobs.Job12;
            JobContracts.JobContract2.FreelancerId = Users.FreelancerSecond.Id;

            JobContracts.JobContract3.JobId = Jobs.Job13.Id;
            JobContracts.JobContract3.Job = Jobs.Job13;
            JobContracts.JobContract3.FreelancerId = Users.FreelancerFirst.Id;

            JobContracts.JobContract4.JobId = Jobs.Job18.Id;
            JobContracts.JobContract4.Job = Jobs.Job18;
            JobContracts.JobContract4.FreelancerId = Users.FreelancerFirst.Id;

            JobContracts.JobContract5.JobId = Jobs.Job19.Id;
            JobContracts.JobContract5.Job = Jobs.Job19;
            JobContracts.JobContract5.FreelancerId = Users.FreelancerSecond.Id;

        }

        private static void BuildJobBidChatMessages()
        {
            JobBidChatMessagesMock.jobBidChatMessages1.JobBidChatSessionId =JobBidSessionMock.JobBidChatSessions1.Id;
            JobBidChatMessagesMock.jobBidChatMessages1.SenderId =Users.FreelancerFirst.Id;

            JobBidChatMessagesMock.jobBidChatMessages2.JobBidChatSessionId = JobBidSessionMock.JobBidChatSessions2.Id;
            JobBidChatMessagesMock.jobBidChatMessages2.SenderId = Users.FreelancerSecond.Id;

            JobBidChatMessagesMock.jobBidChatMessages3.JobBidChatSessionId = JobBidSessionMock.JobBidChatSessions1.Id;
            JobBidChatMessagesMock.jobBidChatMessages3.SenderId = Users.FreelancerSecond.Id;

            JobBidChatMessagesMock.jobBidChatMessages4.JobBidChatSessionId = JobBidSessionMock.JobBidChatSessions2.Id;
            JobBidChatMessagesMock.jobBidChatMessages4.SenderId = Users.FreelancerFirst.Id;

            JobBidChatMessagesMock.jobBidChatMessages5.JobBidChatSessionId = JobBidSessionMock.JobBidChatSessions2.Id;
            JobBidChatMessagesMock.jobBidChatMessages5.SenderId = Users.FreelancerFirst.Id;

            foreach (var message in _jobBidChatMessages)
            {
                var user = _users.First(x => x.Id == message.SenderId);
                message.Sender = user;
            }
            foreach (var message in _jobBidChatMessages)
            {
                var session = _jobBidChatSession.First(x => x.Id == message.JobBidChatSessionId);
                message.JobBidChatSession = session;
            }


        }
        private static void BuildJobBidChatSessionUser()
        {
            JobBidChatSessionUsersMock.JobBidChatSessionUser1.JobBidChatSessionId = JobBidSessionMock.JobBidChatSessions1.Id;
            JobBidChatSessionUsersMock.JobBidChatSessionUser1.UserId = Users.FreelancerFirst.Id;

            JobBidChatSessionUsersMock.JobBidChatSessionUser2.JobBidChatSessionId = JobBidSessionMock.JobBidChatSessions2.Id;
            JobBidChatSessionUsersMock.JobBidChatSessionUser2.UserId = Users.FreelancerSecond.Id;

            foreach (var user in _jobBidChatSessionUsers)
            {
                var users = _users.First(x => x.Id == user.UserId);
                user.User = users;
            }
            foreach (var user in _jobBidChatSessionUsers)
            {
                var session = _jobBidChatSession.First(x => x.Id == user.JobBidChatSessionId);
                user.JobBidChatSession = session;
            }
        }

        private static void BuildJobBidChatSessions()
        {
            JobBidSessionMock.JobBidChatSessions1.Job = Jobs.Job3;
            JobBidSessionMock.JobBidChatSessions1.JobId = Jobs.Job3.Id;
            JobBidSessionMock.JobBidChatSessions1.ClientId = Users.CustomerSecond.Id;
            JobBidSessionMock.JobBidChatSessions1.FreelancerId = Users.FreelancerFirst.Id;

            JobBidSessionMock.JobBidChatSessions2.Job = Jobs.Job4;
            JobBidSessionMock.JobBidChatSessions2.JobId = Jobs.Job4.Id;
            JobBidSessionMock.JobBidChatSessions2.ClientId = Users.CustomerFirst.Id;
            JobBidSessionMock.JobBidChatSessions2.FreelancerId = Users.FreelancerSecond.Id;

            foreach (var session in _jobBidChatSession)
            {
                var job = _jobs.First(x => x.Id == session.JobId);
                session.Job = job;
            }

            foreach (var session in _jobBidChatSession)
            {
                var client = _users.First(x => x.Id == session.ClientId);
                session.Client = client;
            }
            foreach (var session in _jobBidChatSession)
            {
                var freelancer = _users.First(x => x.Id == session.FreelancerId);
                session.Freelancer = freelancer;
            }
        }

        private static void BuildContractChatMessages()
        {
            ContractChatMessagesMock.ContractChatMessages1.ContractChatSessionId = ContractChatSessionsMock.ContractChatSessions1.Id;
            ContractChatMessagesMock.ContractChatMessages1.SenderId = Users.FreelancerFirst.Id;

            ContractChatMessagesMock.ContractChatMessages2.ContractChatSessionId = ContractChatSessionsMock.ContractChatSessions2.Id;
            ContractChatMessagesMock.ContractChatMessages2.SenderId = Users.FreelancerSecond.Id;

            ContractChatMessagesMock.ContractChatMessages3.ContractChatSessionId = ContractChatSessionsMock.ContractChatSessions1.Id;
            ContractChatMessagesMock.ContractChatMessages3.SenderId = Users.FreelancerSecond.Id;

            ContractChatMessagesMock.ContractChatMessages4.ContractChatSessionId = ContractChatSessionsMock.ContractChatSessions1.Id;
            ContractChatMessagesMock.ContractChatMessages4.SenderId = Users.FreelancerSecond.Id;

            ContractChatMessagesMock.ContractChatMessages5.ContractChatSessionId = ContractChatSessionsMock.ContractChatSessions2.Id;
            ContractChatMessagesMock.ContractChatMessages5.SenderId = Users.FreelancerFirst.Id;

            foreach (var message in _contractChatMessages)
            {
                var user = _users.First(x => x.Id == message.SenderId);
                message.User = user;
            }
            foreach (var message in _contractChatMessages)
            {
                var session = _contractChatSessions.First(x => x.Id == message.ContractChatSessionId);
                message.ContractChatSession = session;
            }
        }

        private static void BuildContractChatSessionUser()
        {
            ContractChatSessionUsersMock.ContractChatSessionUsers1.ContractChatSessionId =ContractChatSessionsMock.ContractChatSessions1.Id;
            ContractChatSessionUsersMock.ContractChatSessionUsers1.UserId = Users.CustomerFirst.Id;

            ContractChatSessionUsersMock.ContractChatSessionUsers2.ContractChatSessionId = ContractChatSessionsMock.ContractChatSessions2.Id;
            ContractChatSessionUsersMock.ContractChatSessionUsers2.UserId = Users.CustomerSecond.Id;

            foreach (var user in _contractChatSessionUsers)
            {
                var users = _users.First(x => x.Id == user.UserId);
                user.UserDto = users;
            }
            foreach (var user in _contractChatSessionUsers)
            {
                var session = _contractChatSessions.First(x => x.Id == user.ContractChatSessionId);
                user.ContractChatSessionDto = session;
            }
        }

        private static void BuildContractChatSessions()
        {
            ContractChatSessionsMock.ContractChatSessions1.jobDto = Jobs.Job1;
            ContractChatSessionsMock.ContractChatSessions1.JobId = Jobs.Job1.Id;
            ContractChatSessionsMock.ContractChatSessions1.ClientId = Users.CustomerFirst.Id;
            ContractChatSessionsMock.ContractChatSessions1.FreelancerId = Users.FreelancerFirst.Id;

            ContractChatSessionsMock.ContractChatSessions2.jobDto = Jobs.Job2;
            ContractChatSessionsMock.ContractChatSessions2.JobId = Jobs.Job2.Id;
            ContractChatSessionsMock.ContractChatSessions2.ClientId = Users.CustomerSecond.Id;
            ContractChatSessionsMock.ContractChatSessions2.FreelancerId = Users.FreelancerSecond.Id;

            foreach (var session in _contractChatSessions)
            {
                var job = _jobs.First(x => x.Id == session.JobId);
                session.jobDto = job;
            }
            foreach (var session in _contractChatSessions)
            {
                var message = _contractChatMessages.Where(x => x.ContractChatSessionId == session.Id).ToList();

                if (message.Any())
                {
                    session.ContractChatMessagesDto = message;
                }
            }

        }

        private static void BuildJobAttachments()
        {
            JobAttachments.JobAttachment1.Job = Jobs.Job1;
            JobAttachments.JobAttachment1.JobId = Jobs.Job1.Id;

            JobAttachments.JobAttachment2.Job = Jobs.Job2;
            JobAttachments.JobAttachment2.JobId = Jobs.Job2.Id;
            JobAttachments.JobAttachment3.Job = Jobs.Job3;
            JobAttachments.JobAttachment3.JobId = Jobs.Job3.Id;

            JobAttachments.JobAttachment4.Job = Jobs.Job1;
            JobAttachments.JobAttachment4.JobId = Jobs.Job1.Id;
            JobAttachments.JobAttachment5.Job = Jobs.Job2;
            JobAttachments.JobAttachment5.JobId = Jobs.Job2.Id;

            JobAttachments.JobAttachment6.Job = Jobs.Job3;
            JobAttachments.JobAttachment6.JobId = Jobs.Job3.Id;

            JobAttachments.JobAttachment7.Job = Jobs.Job1;
            JobAttachments.JobAttachment7.JobId = Jobs.Job1.Id;

            JobAttachments.JobAttachment8.Job = Jobs.Job2;
            JobAttachments.JobAttachment8.JobId = Jobs.Job2.Id;

            JobAttachments.JobAttachment9.Job = Jobs.Job3;
            JobAttachments.JobAttachment9.JobId = Jobs.Job3.Id;

            JobAttachments.JobAttachment10.Job = Jobs.Job1;
            JobAttachments.JobAttachment10.JobId = Jobs.Job1.Id;

            foreach (var job in _jobs)
            {
                var attachments = _jobAttachments.Where(x => x.JobId == job.Id).ToList();

                if (attachments.Any())
                {
                    job.JobAttachments = attachments;
                }
            }
        }

        /// <summary>
        /// Note that customer and freelancers have been separated
        /// At present, customers cannot be freelancers and freelancers cannot be customers
        /// </summary>
        private static void BuildJobBids()
        {
            JobBids.JobBid1.JobId = Jobs.Job1.Id;
            JobBids.JobBid2.JobId = Jobs.Job2.Id;
            JobBids.JobBid3.JobId = Jobs.Job3.Id;
            JobBids.JobBid4.JobId = Jobs.Job4.Id;
            JobBids.JobBid5.JobId = Jobs.Job5.Id;
            JobBids.JobBid6.JobId = Jobs.Job6.Id;
            JobBids.JobBid7.JobId = Jobs.Job7.Id;
            JobBids.JobBid8.JobId = Jobs.Job8.Id;
            JobBids.JobBid9.JobId = Jobs.Job8.Id;
            JobBids.JobBid10.JobId = Jobs.Job8.Id;
            JobBids.JobBid11.JobId = Jobs.Job9.Id;
            JobBids.JobBid12.JobId = Jobs.Job10.Id;

            JobBids.JobBid1.User = Users.FreelancerFirst;
            JobBids.JobBid2.User = Users.FreelancerSecond;
            JobBids.JobBid3.User = Users.FreelancerFirst;
            JobBids.JobBid4.User = Users.FreelancerSecond;
            JobBids.JobBid5.User = Users.FreelancerFirst;
            JobBids.JobBid6.User = Users.FreelancerSecond;
            JobBids.JobBid7.User = Users.FreelancerFirst;
            JobBids.JobBid8.User = Users.FreelancerSecond;
            JobBids.JobBid9.User = Users.FreelancerFirst;
            JobBids.JobBid10.User = Users.FreelancerSecond;
            JobBids.JobBid11.User = Users.FreelancerFirst;
            JobBids.JobBid12.User = Users.FreelancerSecond;

            JobBids.JobBid1.FreelancerUserId = Users.FreelancerFirst.Id;
            JobBids.JobBid2.FreelancerUserId = Users.FreelancerSecond.Id;
            JobBids.JobBid3.FreelancerUserId = Users.FreelancerFirst.Id;
            JobBids.JobBid4.FreelancerUserId = Users.FreelancerSecond.Id;
            JobBids.JobBid5.FreelancerUserId = Users.FreelancerFirst.Id;
            JobBids.JobBid6.FreelancerUserId = Users.FreelancerSecond.Id;
            JobBids.JobBid7.FreelancerUserId = Users.FreelancerFirst.Id;
            JobBids.JobBid8.FreelancerUserId = Users.FreelancerSecond.Id;
            JobBids.JobBid9.FreelancerUserId = Users.FreelancerFirst.Id;
            JobBids.JobBid10.FreelancerUserId = Users.FreelancerSecond.Id;
            JobBids.JobBid11.FreelancerUserId = Users.FreelancerFirst.Id;
            JobBids.JobBid12.FreelancerUserId = Users.FreelancerSecond.Id;

            foreach (var job in _jobs)
            {
                var bids = _jobBids.Where(x => x.JobId == job.Id).ToList();

                if (bids.Any())
                {
                    job.JobBids = bids;
                }
            }

            foreach (var bid in _jobBids)
            {
                var job = _jobs.First(x => x.Id == bid.JobId);
                bid.Job = job;
            }
        }

        private static void BuildUsers()
        {

            foreach (var user in _users)
            {
                var jobs = _jobs.Where(x => x.UserId == user.Id).ToList();

                if (jobs.Any())
                {
                    user.Jobs = jobs;
                }

                var jobBids = _jobBids.Where(x => x.FreelancerUserId == user.Id).ToList();

                if (jobBids.Any())
                {
                    user.JobBids = jobBids;
                }
                
                var balances = _userBalances.Where(x => x.UserId == user.Id).ToList();

                if (balances.Any())
                {
                    user.UserBalances = balances;
                }

                var payments = _userPayments.Where(x => x.FromUserId == user.Id).ToList();

                if (payments.Any())
                {
                    user.UserPayments = payments;
                }
                
                var withdrawals = _userWithdrawals.Where(x => x.UserId == user.Id).ToList();

                if (withdrawals.Any())
                {
                    user.UserWithdrawals = withdrawals;
                }
            }
        }

        private static void BuildJobContract()
        {
            #region SetJobId
            Contract.HiredJob1.JobId = Jobs.Job17.Id;
           Contract.HiredJob2.JobId = Jobs.Job16.Id;

            #endregion
            #region SetClientId
            Contract.HiredJob1.ClientId = Users.CustomerFirst.Id;
            Contract.HiredJob2.ClientId= Users.CustomerFirst.Id;
            
            #endregion
            #region SetFreelancerId
            Contract.HiredJob1.FreelancerId = Users.CustomerFirst.Id;
            Contract.HiredJob2.FreelancerId = Users.CustomerFirst.Id;
            #endregion

        }

        private static void BuildJobBidAttachments()
        {
            JobBidAttachments.JobBidAttachment1.JobBidId= JobBids.JobBid1.Id;
            JobBidAttachments.JobBidAttachment2.JobBidId = JobBids.JobBid1.Id;
            JobBidAttachments.JobBidAttachment3.JobBidId = JobBids.JobBid1.Id;

            JobBidAttachments.JobBidAttachment4.JobBidId = JobBids.JobBid2.Id;
            JobBidAttachments.JobBidAttachment5.JobBidId = JobBids.JobBid2.Id;
            JobBidAttachments.JobBidAttachment6.JobBidId = JobBids.JobBid2.Id;

            JobBidAttachments.JobBidAttachment7.JobBidId = JobBids.JobBid3.Id;
            JobBidAttachments.JobBidAttachment8.JobBidId = JobBids.JobBid3.Id;
            JobBidAttachments.JobBidAttachment9.JobBidId = JobBids.JobBid3.Id;

            JobBidAttachments.JobBidAttachment10.JobBidId = JobBids.JobBid4.Id;

            JobBidAttachments.JobBidAttachment1.JobBidId = JobBids.JobBid5.Id;
            JobBidAttachments.JobBidAttachment2.JobBidId = JobBids.JobBid5.Id;
            JobBidAttachments.JobBidAttachment3.JobBidId = JobBids.JobBid5.Id;

            JobBidAttachments.JobBidAttachment4.JobBidId = JobBids.JobBid6.Id;
            JobBidAttachments.JobBidAttachment5.JobBidId = JobBids.JobBid6.Id;

            JobBidAttachments.JobBidAttachment6.JobBidId = JobBids.JobBid7.Id;
            JobBidAttachments.JobBidAttachment7.JobBidId = JobBids.JobBid7.Id;

            JobBidAttachments.JobBidAttachment8.JobBidId = JobBids.JobBid8.Id;
            JobBidAttachments.JobBidAttachment9.JobBidId = JobBids.JobBid8.Id;

            JobBidAttachments.JobBidAttachment10.JobBidId = JobBids.JobBid9.Id;

            JobBidAttachments.JobBidAttachment1.JobBidId = JobBids.JobBid10.Id;


            foreach (var jobBid in _jobBids)
            {
                var attachments = _jobBidAttachment.Where(x => x.JobBidId == jobBid.Id).ToList();

                if (attachments.Any())
                {
                    jobBid.JobBidAttachments = attachments;
                }
            }
        }

        private static void BuildMilestoneAttachments()
        {
            MilestoneAttachmentMock.milestoneAttachment1.MilestoneId = Milestones.Milestone1.Id;
            MilestoneAttachmentMock.milestoneAttachment2.MilestoneId = Milestones.Milestone1.Id;

            MilestoneAttachmentMock.milestoneAttachment3.MilestoneId = Milestones.Milestone2.Id;
            MilestoneAttachmentMock.milestoneAttachment4.MilestoneId = Milestones.Milestone2.Id;

            MilestoneAttachmentMock.milestoneAttachment5.MilestoneId = Milestones.Milestone3.Id;
            MilestoneAttachmentMock.milestoneAttachment6.MilestoneId = Milestones.Milestone3.Id;

            MilestoneAttachmentMock.milestoneAttachment7.MilestoneId = Milestones.Milestone4.Id;
            MilestoneAttachmentMock.milestoneAttachment8.MilestoneId = Milestones.Milestone4.Id;

            MilestoneAttachmentMock.milestoneAttachment9.MilestoneId = Milestones.Milestone5.Id;
            MilestoneAttachmentMock.milestoneAttachment10.MilestoneId = Milestones.Milestone5.Id;



            foreach (var milestone in _milestones)
            {
                var attachments = _milestoneAttachments.Where(x => x.MilestoneId == milestone.Id).ToList();

                if (attachments.Any())
                {
                    milestone.MilestoneAttachments = attachments;
                }
            }
        }


        private static void BuildEmailAttachments() {

            EmailAttachmentsMock.emailAttachments1.EmailAuditId = EmailAuditMock.EmailAudit1.Id;
            EmailAttachmentsMock.emailAttachments2.EmailAuditId = EmailAuditMock.EmailAudit2.Id;
            EmailAttachmentsMock.emailAttachments3.EmailAuditId = EmailAuditMock.EmailAudit3.Id;
            EmailAttachmentsMock.emailAttachments3.EmailAuditId = EmailAuditMock.EmailAudit3.Id;

            foreach (var emailAudit in _emailAudits)
            {
                var attachments = _emailAttachments.Where(x => x.EmailAuditId == emailAudit.Id).ToList();

                if (attachments.Any())
                {
                    emailAudit.EmailAttachmentsDto = attachments;
                }
            }

        }

        private static void BuildCards()
        {
            foreach (var card in _cards)
            {
                var user = _users.First(x => x.Id == card.UserId);
                card.User = user;
            }
        }

        private static void BuildAccountDetails()
        {
            AccountDetailMock.AccountDetail1.UserId = Users.FreelancerFirst.Id;
            AccountDetailMock.AccountDetail2.UserId = Users.FreelancerSecond.Id;
            AccountDetailMock.AccountDetail3.UserId = Users.FreelancerFirst.Id;
            AccountDetailMock.AccountDetail4.UserId = Users.FreelancerSecond.Id;
            AccountDetailMock.AccountDetail5.UserId = Users.FreelancerFirst.Id;

            foreach (var accountDetail in _accountDetails)
            {
                var user = _users.First(x => x.Id == accountDetail.UserId);
                accountDetail.User = user;
            }
        }
    }
}
