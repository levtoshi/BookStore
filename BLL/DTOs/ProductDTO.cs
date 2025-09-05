namespace BLL.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int Cost { get; set; }
        public int Price { get; set; }
        public BookDTO Book { get; set; }
        public DiscountDTO? Discount { get; set; }
        public DelayDTO? DelayedForCustomer { get; set; }
    }
}