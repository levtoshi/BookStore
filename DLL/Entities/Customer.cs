using System.ComponentModel.DataAnnotations;

namespace DLL.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public FullName FullName { get; set; }
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }
    }
}