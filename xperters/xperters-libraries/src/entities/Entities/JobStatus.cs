using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xperters.entities.Entities
{
    public class JobStatus
    {
        [Key]
        public int JobStatusId { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }       
    }
}
