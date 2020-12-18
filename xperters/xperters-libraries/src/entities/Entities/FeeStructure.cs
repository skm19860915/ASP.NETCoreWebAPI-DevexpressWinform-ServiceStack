using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xperters.entities.Entities
{
    public class FeeStructure: BaseEntity
    {
        [Column(TypeName = "varchar(100)")]
        [Required]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal BandStart { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal BandEnd { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal FeeFlatRate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal FeePercentage { get; set; }                
    }   
}