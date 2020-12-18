using System;
using System.Collections.Generic;

namespace xperters.domain
{
    public class MilestoneDto : BaseDto
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public int MilestoneStatus { get; set; }
        public Guid CreatedId { get; set; }
        public virtual UserDto CreatedBy { get; set; }


        public Guid ContractId { get; set; }

        public JobContractDto Contract { get; set; }

        public List<MilestoneAttachmentDto> MilestoneAttachments { get; set; }
        public List<ContractMilestoneFundDto> ContractFunds { get; set; }
        public List<MilestoneMessageDto> MilestoneMessages { get; set; }
        public List<MilestoneRequestPayerDto> MilestoneRequestPayers { get; set; }
        public List<MilestoneSystemRequestPayerDto> MilestoneSystemRequestPayers { get; set; }
    }
}
