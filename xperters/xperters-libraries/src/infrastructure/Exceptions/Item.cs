using System.ComponentModel.DataAnnotations;

namespace xperters.infrastructure.Exceptions
{
    public class Item
    {
        [Required]
        public string Name { get; set; }

        [Range(1, 10)]
        public int Rating { get; set; }
    }
}