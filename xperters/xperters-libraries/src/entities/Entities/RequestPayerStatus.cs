using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace xperters.entities.Entities
{
    public class RequestPayerStatus
    {
        [Key]
        public int PayerStatusId { get; set; }
        public string PayerStatus { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<MilestoneRequestPayer> MilestoneRequestPayers { get; set; }
    }
}
