using System.ComponentModel.DataAnnotations;

namespace xperters.entities.Entities
{
    public class Currency
    {
        [Key]
        public int CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
    }
}
