namespace BLL.DTOs
{
    public class DelayDTO
    {
        public int Id { get; set; }
        public CustomerDTO Customer { get; set; }
        public int Amount { get; set; }
    }
}