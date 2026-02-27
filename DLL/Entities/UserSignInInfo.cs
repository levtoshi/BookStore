using System.ComponentModel.DataAnnotations;

namespace DLL.Entities
{
    public class UserSignInInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Login { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}