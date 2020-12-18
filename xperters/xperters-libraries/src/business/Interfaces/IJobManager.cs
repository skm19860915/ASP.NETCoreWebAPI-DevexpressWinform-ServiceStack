using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using xperters.domain;
using xperters.models;

namespace xperters.business.Interfaces
{
    public interface IJobManager
    {
        ResultModel<JobDto> GetJobList(int pageNo, List<SearchFilter> searchFilter, int pageSize);
        ResultModel<JobDto> AddJob(JobDto jobDto, UserDto userDto);
        JobDto GetJobById(Guid jobId);
        Guid GetJobIdByContractId(Guid contractId);
        ResultModel<SkillDto> GetSkills();
        ResultModel<CategoryDto> GetCategories();
        JobAttachmentDto GetAttachment(Guid id);
        List<JobAttachmentDto> GetAttachments(Guid jobId);
        ResultModel SubmitBid(JobBidDto jobBidDto);
        ResultModel<JobDto> GetMyJobList(int pageNo, int pageSize, int jobStatus);
        ResultModel<JobBidDto> GetBidsByJobId(Guid jobId, int status);
        JobBidDto GetHiredBid(Guid jobid, Guid userId);
        JobBidAttachmentDto GetJobBidAttachment(Guid id);
        ResultModel<BidNegotiationDto> BidNegotiation(BidNegotiationDto bidNegotiationDto);
        ResultModel HireFreeLancer(HiredJobDto HiredJobDto);
        ResultModel<JobBidDto> GetMyBids(Guid UserId);

        JobBidDto GetBidDetail(Guid jobId,Guid bidId);
       
       
        CountModel GetCount(Guid id);
        JobDto GetMyJobCount();
        ResultModel<MilestoneDto> SubmitMilestone(MilestoneDto milestoneDto);
        ResultModel<MilestoneDto> GetMilestone(int pageNo, int pageSize, Guid jobId);

        ResultModel<JobDto> GetFreelancerJobList(int pageNo, int pageSize, int jobStatus);
        JobDto FreelancerJobscount();

        ResultModel UpdateMilestone(MilestoneDto milestoneDto);
        MilestoneAttachmentDto GetMilestoneAttachment(Guid id);
        ResultModel CloseJob(Guid jobId);

        ResultModel UpdateJobSatus( Guid jobid, int jobStatus);
        Task<ResultModel> SendEmailToClient(JobBidDto jobBidDto);
        Task<ResultModel> SendEmailToFreeLancer(Guid freelancerId);
        Task<ResultModel> SendEmailToFreeLancerForNegotiation(Guid freelancerId, bool test);
        Task<ResultModel> CompleteMilestoneEmailNotification(MilestoneDto milestoneDto);
        Task<ResultModel> CompleteJobNotification(Guid contractId, bool test);
        ResultModel GetActiveMilestoneCount(Guid jobContractId,bool test);
        bool HasAlreadyBid(Guid id);
        List<JobBidChatMessageDto> GetChatsMessage(Guid id, Guid clientId, int messageType, Guid freelancerId);
        ResultModel<JobBidChatMessageDto> SaveChatMessage(JobBidChatMessageView jobBidChatMessageView);

        UserDto GetUserDetails(Guid userId);

        Guid GetFreelancerIdByJobId(Guid jobId);
    }
}
