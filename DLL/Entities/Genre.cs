using System.ComponentModel.DataAnnotations;

namespace DLL.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}