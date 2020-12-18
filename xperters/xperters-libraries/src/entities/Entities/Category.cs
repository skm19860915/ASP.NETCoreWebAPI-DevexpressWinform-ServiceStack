using System.ComponentModel.DataAnnotations;

namespace xperters.entities.Entities
{
    public class Category : BaseEntityMaster
    {     
        [Required]
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
