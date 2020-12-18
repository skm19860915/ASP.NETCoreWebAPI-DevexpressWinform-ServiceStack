using System.ComponentModel.DataAnnotations;

namespace xperters.entities.Entities
{
   public class Skill: BaseEntityMaster
    {
        [Required]
        public string SkillName { get; set; }
        public bool IsActive { get; set; }
    }
}
