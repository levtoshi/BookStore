using System.ComponentModel.DataAnnotations;

namespace DLL.Entities
{
    public class Delay
    {
        public int Id { get; set; }
        [Required]
        public Customer Customer { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}