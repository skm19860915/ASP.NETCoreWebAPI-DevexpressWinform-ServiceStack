using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xperters.entities.Entities
{
    public class Country : BaseEntityMaster
    {     
        [Required]
        [Column(TypeName = "varchar(150)")]
        public string CountryName { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(2)")]
        public string CountryCode { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
