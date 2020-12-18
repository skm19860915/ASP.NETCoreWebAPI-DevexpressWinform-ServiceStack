using System;
using System.Collections.Generic;
using xperters.domain;

namespace xperters.business.Interfaces
{
    public interface IMilestoneManager
    {
        ResultModel<MilestoneDto> GetMilestoneDetail(Guid jobId);
        ResultModel<MilestoneMessageDto> SubmitMilestoneMessage(MilestoneMessageDto milestoneMessageDto);
        ResultModel<MilestoneMessageDto> GetMilestoneMessage(Guid milestoneId);
        IEnumerable<MilestoneDto> GetJobMilestones(Guid jobId);
        IEnumerable<MilestoneRequestPayerDto> GetMilestoneRequestPayers(Guid milestoneId);
        IEnumerable<MilestonePaymentDto> GetMilestonePaymentsForApproval(int page, int numberPerPage);
        List<Guid> UpdateMilestonePaymentsForAdminApproval(List<Guid> milestoneIdsToApprove);
        IEnumerable<MilestoneSystemRequestPayerDto> GetMilestoneSystemRequestPayers(Guid milestoneId);
        bool UpdateMilestoneStatus(Guid milestoneId);
    }
}
