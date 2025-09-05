namespace BookStoreUI.Models.ControlContexts
{
    public class ChangeBookStockControlContext
    {
        public bool IsWriteOffMode { get; set; }
        public ChangeBookStockControlContext()
        {
            IsWriteOffMode = false;
        }
    }
}