using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace xperters.entities.Entities
{
    public abstract class BaseEntity : BaseReadOnlyEntity
    {
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        protected BaseEntity()
        {
            ModifiedDate = DateTime.UtcNow;
        }
    }
}
