using BookStoreUI.Models.ControlContexts;

namespace BookStoreUI.Stores.ControlContextStores
{
    public class ChangeBookStockControlContextStore
    {
        private readonly ChangeBookStockControlContext _changeBookStockControlContextObject;
        public ChangeBookStockControlContext ChangeBookStockControlContextObject
        {
            get
            {
                return _changeBookStockControlContextObject;
            }
        }

        public ChangeBookStockControlContextStore()
        {
            _changeBookStockControlContextObject = new ChangeBookStockControlContext();
        }
    }
}