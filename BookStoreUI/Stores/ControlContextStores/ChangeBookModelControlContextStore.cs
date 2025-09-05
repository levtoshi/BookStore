using BookStoreUI.Models.ControlContexts;

namespace BookStoreUI.Stores.ControlContextStores
{
    public class ChangeBookModelControlContextStore
    {
        private readonly ChangeBookModelControlContext _changeBookModelControlContextObject;
        public ChangeBookModelControlContext ChangeBookModelControlContextObject
        {
            get
            {
                return _changeBookModelControlContextObject;
            }
        }

        public ChangeBookModelControlContextStore()
        {
            _changeBookModelControlContextObject = new ChangeBookModelControlContext();
        }
    }
}