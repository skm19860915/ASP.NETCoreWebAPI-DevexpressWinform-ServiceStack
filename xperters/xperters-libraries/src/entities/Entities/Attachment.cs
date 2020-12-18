using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xperters.entities.Entities
{
   public abstract class Attachment : BaseEntity
    {

        [Column(TypeName = "varchar(2083)")]
        public string Uri { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string MimeType { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string LocalPath { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string FileName { get; set; }

        [Required]
        public long FileSize { get; set; }
    }
}
