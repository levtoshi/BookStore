using DLL.Entities;
using DLL.Models.Collections;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DLL.Stores
{
    public class ProductsStore : IDisposable
    {
        private ControlledObservableCollection<Product> _products;
        public ControlledObservableCollection<Product> Products
        {
            get
            {
                return _products;
            }
        }

        private readonly SemaphoreSlim _setToDefaultSemaphore;
        private readonly SemaphoreSlim _changeProductsSemaphore;
        private int _waitingCount;

        private readonly BookStoreContext _bookStoreContext;

        public Func<Product, bool>? FilterFunc;

        public ProductsStore(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;
            _bookStoreContext.SavedChanges += OnDbContextChanged;
            _products = new ControlledObservableCollection<Product>();

            _setToDefaultSemaphore = new SemaphoreSlim(1);
            _changeProductsSemaphore = new SemaphoreSlim(1);
            //_ = SetToDefault();
        }

        private async void OnDbContextChanged(object? sender, EventArgs e)
        {
            await SetToDefault();
        }

        public async Task SetToDefault()
        {
            FilterFunc ??= p => true;

            _waitingCount++;
            await _setToDefaultSemaphore.WaitAsync();

            Products.BeginSettingDefault();

            IEnumerable<Product> tempProducts = new List<Product>();
            await Task.Run(() =>
            {
                tempProducts = _bookStoreContext.Products.Include(a => a.Book).ThenInclude(b => b.Author).Include(a => a.Book).ThenInclude(b => b.Producer).Include(a => a.Book).ThenInclude(b => b.Genre).Include(a => a.DelayedForCustomer).ThenInclude(b => b.Customer).ThenInclude(c => c.FullName).Include(a => a.Discount);
            });

            await ChangeProducts(tempProducts.Where(p => FilterFunc.Invoke(p)).ToList());

            _waitingCount--;
            _setToDefaultSemaphore.Release();

            if (_waitingCount == 0)
            {
                Products.EndUpdate();
            }
        }

        public async Task<Product?> FindElementAsync(int id)
        {
            return await Task.Run(() => _products.Where(p => p.Id == id).FirstOrDefault());
        }

        public async Task ChangeProducts(List<Product> products)
        {
            _waitingCount++;
            await _changeProductsSemaphore.WaitAsync();
            Products.BeginUpdate();

            Products.Clear();
            //await Task.Delay(2000);
            foreach (var product in products)
            {
                Products.Add(product);
            }

            _waitingCount--;
            _changeProductsSemaphore.Release();

            if (_waitingCount == 0)
            {
                Products.EndUpdate();
            }
        }

        public void Dispose()
        {
            _bookStoreContext.SavedChanges -= OnDbContextChanged;
        }
    }
}