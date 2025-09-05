using System.ComponentModel.DataAnnotations;

namespace DLL.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
        [Required]
        public UserSignInInfo SignInInfo { get; set; }

        [Required]
        public FullName UserFullName { get; set; }
    }
}