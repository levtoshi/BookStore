namespace BLL.DTOs
{
    public class SaleDTO
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime SoldTime { get; set; }
        public ProductDTO Product { get; set; }
    }
}