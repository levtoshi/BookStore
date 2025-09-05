namespace BLL.DTOs
{
    public class DiscountDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Interest { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}