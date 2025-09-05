using System.ComponentModel.DataAnnotations;

namespace DLL.Entities
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public FullName Author { get; set; }
        [Required]
        public Producer Producer { get; set; }
        [Required]
        public short PageAmount { get; set; }
        [Required]
        public Genre Genre { get; set; }
        [Required]
        public short Year { get; set; }
        public string? IsContinuation { get; set; }
    }
}