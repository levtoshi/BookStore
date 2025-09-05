namespace BLL.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FullNameDTO Author { get; set; }
        public ProducerDTO Producer { get; set; }
        public short PageAmount { get; set; }
        public GenreDTO Genre { get; set; }
        public short Year { get; set; }
        public string? IsContinuation { get; set; }
    }
}