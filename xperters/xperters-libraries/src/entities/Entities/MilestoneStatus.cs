using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace xperters.entities.Entities
{
    public class MilestoneStatus
    {
        [Key]
        public int MilestoneStatusId { get; set; }
        public string StatusDescription { get; set; }
        public bool IsActive { get; set; }
    }
}
