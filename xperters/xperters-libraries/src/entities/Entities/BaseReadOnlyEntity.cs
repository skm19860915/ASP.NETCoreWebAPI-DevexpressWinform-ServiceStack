using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xperters.entities.Entities
{
    public abstract class BaseReadOnlyEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        protected BaseReadOnlyEntity()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}