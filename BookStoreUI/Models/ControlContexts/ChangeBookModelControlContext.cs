namespace BookStoreUI.Models.ControlContexts
{
    public class ChangeBookModelControlContext
    {
        public bool IsUpdateMode { get; set; }
        public ChangeBookModelControlContext()
        {
            IsUpdateMode = false;
        }
    }
}