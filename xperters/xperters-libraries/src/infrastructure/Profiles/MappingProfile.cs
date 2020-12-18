using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using xperters.domain;
using xperters.entities.Entities;
using xperters.fileutilities.Blob;
using xperters.infrastructure.Converters;
using xperters.models;
using xperters.configurations;
using xperters.enums;
using Microsoft.VisualBasic;
using xperters.models.DataViews.AdminJob;

namespace xperters.infrastructure.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile(AppConfig appConfig, ILoggerFactory loggerFactory)
        {
            AllowNullDestinationValues = true;
            // Add as many of these lines as you need to map your objects
            CreateMap<JobStatus, JobStatusDto>();
            CreateMap<JobStatusDto, JobStatus>()
                .ForMember(dest => dest.Jobs, src => src.Ignore());
            CreateMap<MilestoneStatus, MilestoneStatusDto>();
            CreateMap<MilestoneStatusDto, MilestoneStatus>();
            CreateMap<Currency, CurrencyDto>();
            CreateMap<CurrencyDto, Currency>();

            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>()
                .ForMember(dest => dest.Users, src => src.Ignore());


            CreateMap<RequestPayerStatus, RequestPayerStatusDto>();
            CreateMap<RequestPayerStatusDto, RequestPayerStatus>()
                .ForMember(dest => dest.MilestoneRequestPayers, src => src.Ignore());

            CreateMap<MilestoneRequestPayerView, MilestoneRequestPayerDto>()
                .ForMember(dest => dest.ClientId, src => src.Ignore())
                .ForMember(dest => dest.Milestone, src => src.Ignore())
                .ForMember(dest => dest.PayerStatus, src => src.Ignore())
                .ForMember(dest => dest.FeeFlat, src => src.Ignore())
                .ForMember(dest => dest.FeePercent, src => src.Ignore())
                .ForMember(dest => dest.TotalAmount, src => src.Ignore())
                .ForMember(dest => dest.PaymentServiceCheckCount, src => src.Ignore())
                .ForMember(dest => dest.CompletedDate, src => src.Ignore());

            CreateMap<MilestoneRequestPayerDto, MilestoneRequestPayerView>();
            CreateMap<MilestoneRequestPayerDto, MilestoneRequestPayer>();
            CreateMap<MilestoneRequestPayer, MilestoneRequestPayerDto>();

            CreateMap<MilestoneSystemRequestPayer, MilestoneSystemRequestPayerDto>();
            CreateMap<MilestoneSystemRequestPayerDto, MilestoneSystemRequestPayer>();

            CreateMap<UserView, UserDto>()
                .ForMember(dest => dest.Jobs, src => src.Ignore())
                .ForMember(dest => dest.Country, src => src.Ignore())
                .ForMember(dest => dest.UserPayments, src => src.Ignore())
                .ForMember(dest => dest.UserBalances, src => src.Ignore())
                .ForMember(dest => dest.UserWithdrawals, src => src.Ignore())
                .ForMember(dest => dest.JobBids, src => src.Ignore())
                .ForMember(dest => dest.UserRole, src => src.MapFrom(x => (Enums.UserRole)x.UserRole))
                .ForMember(dest => dest.ModifiedDate, src => src.Ignore())
                .ForMember(dest => dest.CountryCode, src => src.Ignore());

            CreateMap<UserDto, UserView>()
                .ForMember(dest => dest.MobilePhone, src => src.Ignore())
                .ForMember(dest => dest.UserRole, src => src.MapFrom(x => (int)x.UserRole));

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Country, src => src.Ignore())
                .ForMember(dest => dest.JobBidChatSessionClients, src => src.Ignore())
                .ForMember(dest => dest.JobBidChatSessionsFreelancers, src => src.Ignore())
                .ForMember(dest => dest.JobBidChatSessionUsers, src => src.Ignore())
                .ForMember(dest => dest.JobBids, src => src.Ignore())
                .ForMember(dest => dest.Jobs, src => src.Ignore())
                .ForMember(dest => dest.ContractChatMessages, src => src.Ignore())
                .ForMember(dest => dest.ContractChatSessionClients, src => src.Ignore())
                .ForMember(dest => dest.ContractChatSessionsFreelancers, src => src.Ignore())
                .ForMember(dest => dest.ContractChatSessionUsers, src => src.Ignore())
                .ForMember(dest => dest.JobContracts, src => src.Ignore())
                .ForMember(dest => dest.Milestones, src => src.Ignore())
                .ForMember(dest => dest.ContractFunds, src => src.Ignore())
                .ForMember(dest => dest.EmailAudits, src => src.Ignore())
                .ForMember(dest => dest.Cards, src => src.Ignore())
                .ForMember(dest => dest.UserBalances, src => src.Ignore())
                .ForMember(dest => dest.UserWithdrawals, src => src.Ignore())
                .ForMember(dest => dest.AccountDetails, src => src.Ignore());

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Password, src => src.Ignore())
                .ForMember(dest => dest.Address, src => src.Ignore())
                .ForMember(dest => dest.IsActive, src => src.Ignore())
                .ForMember(dest => dest.CountryCode, src => src.MapFrom(x => x.Country.CountryCode));

            CreateMap<JobView, JobDto>()
                .ForMember(dest => dest.JobAttachments, src => src.Ignore())
                .ForMember(dest => dest.ModifiedDate, src => src.Ignore())
                .ForMember(x => x.JobBids, src => src.Ignore())
                .ForMember(x => x.JobStatus, src => src.Ignore())
                .ForMember(dest => dest.JobBidChatSessions, src => src.Ignore());

            CreateMap<JobDto, JobView>()
                .ForMember(dest => dest.Country, src => src.Ignore())
                .ForMember(dest => dest.FreelancerType, opt => opt.MapFrom(src => EnumHelper.GetDescription((Enums.FreelancerType)src.FreelancerTypeId)))
                .ForMember(dest => dest.FreelancerExperience, opt => opt.MapFrom(src => EnumHelper.GetDescription((Enums.FreelancerExperience)src.ExperienceLevel)))
                .ForMember(dest => dest.JobType, opt => opt.MapFrom(src => EnumHelper.GetDescription((JobEnums.JobType)src.JobTypeId)))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => EnumHelper.GetDescription((JobEnums.JobDuration)src.JobDuration)))
                .ForMember(dest => dest.EstimatedBudget, opt => opt.MapFrom(src => EnumHelper.GetDescription((Enums.EstimatedBudget)src.EstimatedBudgetId)))
                .ForMember(dest => dest.JobAttachments, opt => opt.MapFrom(src => src.JobAttachments))
                .ForMember(dest => dest.JobBidView, opt => opt.Ignore())
                .ForMember(dest => dest.MessageRoomViews, src => src.Ignore());

            CreateMap<SearchFilterView, SearchFilter>();
            CreateMap<JobDto, Job>()
                .ForMember(dest => dest.User, src => src.Ignore()).ForMember(x => x.JobBids, src => src.Ignore())
                .ForMember(x => x.JobAttachments, src => src.Ignore()).ForMember(x => x.ModifiedDate, src => src.Ignore())
                .ForMember(dest => dest.JobBidChatSessions, src => src.Ignore())
                .ForMember(dest => dest.ContractChatSessions, src => src.Ignore());

            CreateMap<Job, JobDto>()
                .ForMember(x => x.JobBids, src => src.Ignore())
                .ForMember(x => x.JobAttachments, src => src.Ignore())
                .ForMember(x => x.JobAttachments, src => src.Ignore())
                .ForMember(x => x.ModifiedDate, src => src.Ignore())
                .ForMember(dest => dest.JobBidChatSessions, src => src.Ignore())
                .ForMember(dest => dest.PostJobCount, src => src.Ignore())
                .ForMember(dest => dest.InprogressJobCount, src => src.Ignore())
                .ForMember(dest => dest.CompletedJobCount, src => src.Ignore())
                .ForMember(dest => dest.CanceledJobCount, src => src.Ignore());

            CreateMap<HireJobView, HiredJobDto>()
                .ForMember(dest => dest.ContractChatSessionId, src => src.Ignore());

            CreateMap<HiredJobDto, HireJobView>()
                .ForMember(dest => dest.ContractChatSessionsId, src => src.Ignore())
                .ForMember(dest => dest.BidStatus, src => src.Ignore());

            CreateMap<HiredJobDto, ContractChatSession>()
               .ForMember(x => x.Job, src => src.Ignore())
                 .ForMember(x => x.Freelancer, src => src.Ignore())
                 .ForMember(x => x.Client, src => src.Ignore())
                 .ForMember(x => x.ContractChatMessages, src => src.Ignore())
                 .ForMember(x => x.ContractChatSessionUsers, src => src.Ignore());

            CreateMap<HiredJobDto, ContractChatSessionUser>()
                .ForMember(d => d.ContractChatSessionId, opts => opts.MapFrom(x => x.ContractChatSessionId))
                .ForMember(d => d.UserId, opts => opts.MapFrom(x => x.ClientId))
                .ForMember(x => x.ContractChatSession, src => src.Ignore())
                .ForMember(x => x.User, src => src.Ignore());

            CreateMap<HiredJobDto, ContractChatMessage>()
                 .ForMember(x => x.SenderId, opts => opts.MapFrom(x => x.ClientId))
                 .ForMember(x => x.ContractChatSessionId, opts => opts.MapFrom(x => x.ContractChatSessionId))
                 .ForMember(x => x.MsgType, opts => opts.MapFrom(x => x.messageType))
                 .ForMember(x => x.SenderUser, src => src.Ignore())
                 .ForMember(x => x.ContractChatSession, src => src.Ignore());


            CreateMap<JobBidChatSessionDto, JobBidChatSession>()
                .ForMember(x => x.Job, src => src.Ignore())
                .ForMember(x => x.JobBidChatMessages, src => src.Ignore())
                .ForMember(x => x.JobBidChatSessionUsers, src => src.Ignore());


            CreateMap<JobBidChatSession, JobBidChatSessionDto>()
                .ForMember(x => x.Job, src => src.Ignore())
                .ForMember(x => x.JobBidChatMessages, src => src.Ignore());


            CreateMap<ContractChatSessionDto, ContractChatSession>()
               .ForMember(x => x.Job, src => src.Ignore())
               .ForMember(x => x.Freelancer, src => src.Ignore())
               .ForMember(x => x.Client, src => src.Ignore())
               .ForMember(x => x.ContractChatSessionUsers, src => src.Ignore())
               .ForMember(x => x.ContractChatMessages, src => src.Ignore());

            CreateMap<ContractChatSessionUserDto, ContractChatSessionUser>()
              .ForMember(x => x.ContractChatSession, src => src.Ignore())
              .ForMember(x => x.User, src => src.Ignore());

            CreateMap<ContractChatMessageDto, ContractChatMessage>()
             .ForMember(x => x.SenderUser, src => src.Ignore())
             .ForMember(x => x.ContractChatSession, src => src.Ignore());

            // for some reason there's a exception when trying to map from user to user.
            // needed investigation later.
            // .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

            // TODO why can't i make the call like this?
            //            CreateMap<JobAttachment, JobAttachmentDto>()
            //                .ConvertUsing<ITypeConverter<JobAttachment, JobAttachmentDto>>();

            CreateMap<AccountDetailDto, AccountDetail>();
            CreateMap<AccountDetail, AccountDetailDto>();
            CreateMap<AccountDetailView, AccountDetailDto>();
            CreateMap<MilestoneMessageView, MilestoneMessageDto>().ForMember(x => x.Id, src => src.Ignore());
            CreateMap<MilestoneMessage, MilestoneMessageDto>().ForMember(x => x.Id, src => src.Ignore())
                .ForMember(x => x.CreatedBy, src => src.Ignore());

            // domain to view mapping
            CreateMap<MilestoneMessageDto, MilestoneMessageView>().ForMember(x => x.Id, src => src.Ignore());

            // domain to entity
            CreateMap<MilestoneMessageDto, MilestoneMessage>()
                .ForMember(x => x.Milestone, s => s.Ignore())
                .ForMember(x => x.Created, s => s.Ignore());

            CreateMap<JobAttachmentDto, JobAttachmentView>();

            CreateMap<JobAttachment, JobAttachmentDto>()
                .ForMember(x => x.Job, s => s.Ignore())
                .ForMember(x => x.FileData, s => s.MapFrom(x => x.Uri))
                .ConvertUsing(new JobAttachmentReader(new BlobService(appConfig, loggerFactory)));

            CreateMap<JobAttachmentDto, JobAttachment>();
            CreateMap<JobBidAttachmentDto, JobBidAttachment>()
                .ForMember(x => x.JobBid, s => s.Ignore());

            CreateMap<JobBid, JobBidDto>()
                .ForMember(x => x.Job, s => s.MapFrom(x => x.Job))
                .ForMember(x => x.JobBidAttachments, s => s.MapFrom(x => x.JobBidAttachments))
                .ForMember(x => x.User, src => src.Ignore())
                .ForMember(dest => dest.JobBidMessages, src => src.Ignore());

            CreateMap<JobBidAttachmentView, JobBidAttachmentDto>();

            CreateMap<JobBidAttachmentDto, JobBidAttachmentView>()
                .ForMember(x => x.JobId, s => s.MapFrom(x => x.JobBid.JobId));

            CreateMap<JobBidDto, JobBid>()
                .ForMember(x => x.FreelancerUser, s => s.Ignore())
                .ForMember(x => x.Job, s => s.Ignore())
                .ForMember(x => x.JobBidAttachments, s => s.MapFrom(x => x.JobBidAttachments));

            CreateMap<JobBidView, JobBidDto>()
                .ForMember(x => x.JobBidMessages, s => s.Ignore());

            CreateMap<JobBidDto, JobBidView>().ForMember(x => x.Job, s => s.MapFrom(x => x.Job))
                .ForMember(x => x.JobBidAttachments, s => s.MapFrom(x => x.JobBidAttachments))
                .ForMember(x => x.User, s => s.MapFrom(x => x.User))
                .ForMember(dest => dest.JobBidMessages, src => src.Ignore());

            CreateMap<JobBidAttachment, JobBidAttachmentDto>().ForMember(x => x.FileData, s => s.MapFrom(x => x.Uri))
                .ConvertUsing(new JobBidAttachmentReader(new BlobService(appConfig, loggerFactory)));

            CreateMap<BidNegotiationView, BidNegotiationDto>().ForMember(x => x.ClientId, s => s.MapFrom(x => x.ClientId))
                .ForMember(x => x.FreelancerId, s => s.MapFrom(x => x.FreelancerId))
                .ForMember(x => x.JobId, s => s.MapFrom(x => x.JobId))
                .ForMember(x => x.Message, s => s.MapFrom(x => x.Message))
                .ForMember(x => x.JobId, s => s.MapFrom(x => x.JobId))
                .ForMember(x => x.JobBidChatSessionId, s => s.MapFrom(x => x.JobBidChatSessionId))
                .ForMember(x => x.BidStatus, s => s.MapFrom(x => x.BidStatus));

            CreateMap<JobBidChatMessageView, JobBidChatSessionDto>()
                .ForMember(dest => dest.Client, src => src.Ignore())
                 .ForMember(dest => dest.ClientId, src => src.Ignore())
                .ForMember(dest => dest.Freelancer, src => src.Ignore())
                .ForMember(dest => dest.Job, src => src.Ignore())
                .ForMember(dest => dest.JobBidChatMessages, src => src.Ignore())
                .ForMember(dest => dest.FreelancerId, src => src.Ignore());

            CreateMap<JobBidChatMessageView, JobBidChatMessageDto>()
                 //.ForMember(x => x.Message, s => s.MapFrom(x => x.Message))
                 //.ForMember(x => x.MessageType, s => s.MapFrom(x => x.MessageType))
                 //.ForMember(x => x.SenderId, s => s.MapFrom(x => x.SenderId))
                 .ForMember(dest => dest.Sender, src => src.Ignore())
                 //.ForMember(dest => dest.SenderType, src => src.Ignore())
                 .ForMember(dest => dest.JobBidChatSession, src => src.Ignore());

            CreateMap<BidNegotiationDto, JobBidChatSession>().ForMember(dest => dest.Job, src => src.Ignore())
                .ForMember(dest => dest.Freelancer, src => src.Ignore())
                .ForMember(dest => dest.Client, src => src.Ignore())
                .ForMember(dest => dest.JobBidChatMessages, src => src.Ignore())
                .ForMember(dest => dest.JobBidChatSessionUsers, src => src.Ignore());

            CreateMap<BidNegotiationDto, JobBidChatMessage>()
                .ForMember(dest => dest.Sender, src => src.Ignore())
                .ForMember(dest => dest.SenderId, src => src.Ignore())
                .ForMember(dest => dest.JobBidChatSession, src => src.Ignore());
            CreateMap<BidNegotiationDto, JobBidChatSessionUser>()
                .ForMember(dest => dest.User, src => src.Ignore())
                .ForMember(dest => dest.UserId, src => src.Ignore())
                .ForMember(dest => dest.JobBidChatSession, src => src.Ignore());

            CreateMap<JobBidChatMessageDto, JobBidChatSessionUser>()
                 .ForMember(dest => dest.User, src => src.Ignore())
                .ForMember(dest => dest.UserId, src => src.Ignore())
                .ForMember(dest => dest.JobBidChatSession, src => src.Ignore());

            CreateMap<JobBidChatSessionUsersDto, JobBidChatSessionUser>();
            CreateMap<JobBidChatMessageDto, JobBidChatMessage>()
                .ForMember(x => x.JobBidChatSession, s => s.MapFrom(x => x.JobBidChatSession));

            CreateMap<JobBidChatMessage, JobBidChatMessageDto>()
                //.ForMember(dest => dest.SenderType, src => src.Ignore())
                .ForMember(x => x.JobBidChatSession, s => s.MapFrom(x => x.JobBidChatSession));

            CreateMap<HiredJobDto, JobContract>()
                .ForMember(x => x.ContractStartDate, s => s.MapFrom(x => x.CreatedDate))
                .ForMember(dest => dest.Job, src => src.Ignore())
                .ForMember(dest => dest.Freelancer, src => src.Ignore())
                .ForMember(dest => dest.Milestones, src => src.Ignore());

            CreateMap<MilestoneView, MilestoneDto>()
                .ForMember(dest => dest.MilestoneAttachments, src => src.Ignore())
                .ForMember(dest => dest.ContractFunds, src => src.Ignore())
                .ForMember(dest => dest.Contract, src => src.Ignore())
                .ForMember(dest => dest.CreatedBy, src => src.Ignore())
                .ForMember(dest => dest.MilestoneAttachments, src => src.Ignore())
                .ForMember(dest => dest.MilestoneMessages, src => src.Ignore())
                .ForMember(dest => dest.MilestoneRequestPayers, src => src.Ignore())
                .ForMember(dest => dest.MilestoneSystemRequestPayers, src => src.Ignore());

            CreateMap<MilestoneDto, MilestoneView>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => EnumHelper.GetDescription((Enums.MilestoneStatus)src.MilestoneStatus)))
                .ForMember(dest => dest.FreelancerStatus, opt => opt.MapFrom(src => EnumHelper.GetFreelancerDescription((Enums.MilestoneStatus)src.MilestoneStatus)))
                .ForMember(x => x.MilestoneAttachments, s => s.Ignore())
                .ForMember(x => x.Due, s => s.Ignore());


            CreateMap<MilestoneDto, Milestone>()
                .ForMember(x => x.MilestoneDescription, s => s.MapFrom(x => x.Description));

            CreateMap<Milestone, MilestoneDto>()
                .ForMember(x => x.ContractFunds, s => s.Ignore())
                .ForMember(x => x.Description, s => s.MapFrom(x => x.MilestoneDescription));

            CreateMap<ContractMilestoneFundDto, ContractMilestoneFund>()
                .ForMember(x => x.Amount, s => s.MapFrom(x => x.Amount))
                .ForMember(x => x.MilestoneId, s => s.MapFrom(x => x.Id))
                .ForMember(dest => dest.FundStatus, src => src.Ignore())
                .ForMember(dest => dest.User, src => src.Ignore())
                .ForMember(dest => dest.Milestone, src => src.Ignore())
                .ForMember(dest => dest.Id, src => src.Ignore());

            CreateMap<MilestoneAttachmentDto, MilestoneAttachment>();

            CreateMap<MilestoneAttachment, MilestoneAttachmentDto>()
                .ForMember(dest => dest.Milestone, src => src.Ignore())
                .ForMember(x => x.FileData, s => s.MapFrom(x => x.Uri))
                .ConvertUsing(new MilestoneAttachmentReader(new BlobService(appConfig, loggerFactory)));

            CreateMap<MilestoneAttachmentDto, MilestoneAttachmentView>()
                .ForMember(dest => dest.Milestone, src => src.Ignore())
                .ForMember(dest => dest.JobId, src => src.Ignore());

            CreateMap<JobContractDto, JobContract>();

            CreateMap<JobContract, JobContractDto>();

            // CardDto and Card are the same thing.  
            // If required, move Card from models library to domain library 
            // then refactor all the code. Not required to have duplicate classes that don't get used.

            CreateMap<models.Card, entities.Entities.Card>()
                .ForMember(dest => dest.User, src => src.Ignore())
                .ForMember(dest => dest.UserId, src => src.Ignore())
                .ForMember(dest => dest.Id, src => src.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(o => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(o => DateTime.UtcNow))
                .ForMember(dest => dest.NumberSuffix, opt => opt.MapFrom(o => Strings.Right(o.Number, 4)));

            CreateMap<entities.Entities.Card, models.Card>()
                .ForMember(dest => dest.MetaData, src => src.Ignore())
                .ForMember(dest => dest.ExtraParams, src => src.Ignore())
                .ForMember(dest => dest.Cvc, src => src.Ignore())
                .ForMember(dest => dest.Expand, src => src.Ignore());

            // view to Dto 
            CreateMap<CardView, CardDto>();
            CreateMap<CardDto, CardView>();
            CreateMap<CategoryDto, CategoryView>();
            CreateMap<Category, CategoryDto>();
            CreateMap<SkillDto, SkillView>();
            CreateMap<SkillView, SkillDto>();
            CreateMap<Skill, SkillDto>();
            CreateMap<SkillDto, Skill>();

            //Dto to Entity
            CreateMap<CardDto, entities.Entities.Card>()
                .ForMember(dest => dest.IsNotSupported, src => src.Ignore())
                .ForMember(dest => dest.Country, src => src.Ignore())
                .ForMember(dest => dest.CardScheme, src => src.Ignore())
                .ForMember(dest => dest.CardType, src => src.Ignore())
                .ForMember(dest => dest.IssuingCardId, src => src.Ignore());

            CreateMap<EmailAuditDto, EmailAudit>()
                .ForMember(dest => dest.User, src => src.Ignore())
                .ForMember(dest => dest.EmailAttachments, src => src.Ignore());

            CreateMap<EmailAttachmentsDto, EmailAttachments>()
                .ForMember(dest => dest.EmailsAudit, src => src.Ignore());

            ConfigPayments();

            ConfigAdminJob();
        }

        private void ConfigAdminJob()
        {

            CreateMap<MilestoneView, MilestoneDetailDto>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(o => o.CreatedDate))
                .ForMember(dest => dest.Modified, opt => opt.MapFrom(o => o.ModifiedDate));

            CreateMap<MilestoneDetailDto, MilestoneView>()
                .ForMember(dest => dest.Due, opt => opt.MapFrom(o => o.DueDate))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(o => o.Created))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(o => o.Modified))
                .ForMember(dest => dest.MilestoneAttachments, src => src.Ignore())
                .ForMember(dest => dest.ContractId, src => src.Ignore())
                .ForMember(dest => dest.CreatedId, src => src.Ignore())
                .ForMember(dest => dest.MilestoneStatus, src => src.Ignore())
                .ForMember(dest => dest.FreelancerStatus, src => src.Ignore());

            CreateMap<JobInformationView, JobInformationDto>();
            CreateMap<JobInformationDto, JobInformationView>();
        }

        private void ConfigPayments()
        {
            CreateMap<SystemPayment, SystemPaymentDto>();

            CreateMap<SystemPaymentDto, SystemPayment>()
                .ForMember(dest => dest.PaymentTransactionType, src => src.Ignore());

            CreateMap<SystemBalanceDto, SystemBalance>();
            CreateMap<SystemBalance, SystemBalanceDto>();

            CreateMap<UserPayment, UserPaymentDto>();


            CreateMap<UserWithdrawal, UserWithdrawalDto>();
            CreateMap<UserWithdrawalDto, UserWithdrawal>();

            CreateMap<UserBalance, UserBalanceDto>();
            CreateMap<UserBalanceDto, UserBalance>()
                .ForMember(dest => dest.UserPayment, src => src.Ignore())
                .ForMember(dest => dest.User, src => src.Ignore());

            CreateMap<UserPaymentDto, UserPayment>()
                .ForMember(dest => dest.PaymentTransactionType, src => src.Ignore())
                .ForMember(dest => dest.UserBalances, src => src.Ignore())
                .ForMember(dest => dest.ToUser, src => src.Ignore())
                .ForMember(dest => dest.FromUser, src => src.Ignore());

            CreateMap<PaymentTransactionType, PaymentTransactionTypeDto>();
            CreateMap<PaymentTransactionTypeDto, PaymentTransactionType>()
                .ForMember(dest => dest.UserWithdrawals, src => src.Ignore())
                .ForMember(dest => dest.SystemPayments, src => src.Ignore())
                .ForMember(dest => dest.UserPayments, src => src.Ignore());

            CreateMap<UserPaymentDto, UserPaymentView>()
                .ForMember(dest => dest.User, src => src.Ignore())
                .ForMember(dest => dest.PaymentId, src => src.Ignore())
                .ForMember(dest => dest.JobDesc, src => src.Ignore());
        }
    }
}
