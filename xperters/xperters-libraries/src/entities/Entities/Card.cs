using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using xperters.models.Enums;

namespace xperters.entities.Entities
{
    public class Card : BaseEntity
    {

        public Card()
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
        }

        public Card(DateTime created)
        {
            CreatedDate = created;
            ModifiedDate = created;
        }

        public bool IsNotSupported { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Country { get; set; }

        [Required]
        public CardScheme CardScheme { get; set; }
        [Required]
        public CardType CardType { get; set; }

        [Required]
        public int ExpMonth { get; set; }

        [Required]
        public int ExpYear { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Number { get; set; }       
        
        [Required]
        [Column(TypeName = "char(4)")]
        public string NumberSuffix { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string AddressCity { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string AddressCountry { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string AddressLine1 { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string AddressLine2 { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string AddressState { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string AddressZip { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Currency { get; set; }


        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string IssuingCardId { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
