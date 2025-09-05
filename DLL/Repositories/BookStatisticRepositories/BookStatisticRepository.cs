using DLL.Entities;
using DLL.Interfaces;
using DLL.Stores;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories.BookStatisticRepositories
{
    public class BookStatisticRepository : IBookStatisticRepository, ISetCollectionToDefaultRepository
    {
        private readonly BookStoreContext _bookStoreContext;
        private readonly ProductsStore _productsStore;
        private IEnumerable<Sale> _sales;

        private int _statisticPeriodFilterID;
        private delegate bool StatisticOperation(Sale s);
        private List<Func<Sale, bool>> _statisticOperationDelegates;

        public BookStatisticRepository(BookStoreContext bookStoreContext,
            ProductsStore productsStore)
        {
            _bookStoreContext = bookStoreContext;
            _productsStore = productsStore;
            _statisticOperationDelegates = new List<Func<Sale, bool>>();

            _statisticOperationDelegates.Add((Sale s) => (s.SoldTime.Date == DateTime.Now.Date));

            _statisticOperationDelegates.Add((Sale s) => (s.SoldTime.DayOfYear > (DateTime.Now.DayOfYear - ((int)DateTime.Now.DayOfWeek)) && s.SoldTime.DayOfYear <= (DateTime.Now.DayOfYear + (7 - (int)DateTime.Now.DayOfWeek))));

            _statisticOperationDelegates.Add((Sale s) => (s.SoldTime.Date.Month == DateTime.Now.Date.Month));
            _statisticOperationDelegates.Add((Sale s) => (s.SoldTime.Date.Year == DateTime.Now.Date.Year));
        }

        public async Task SetToDefault()
        {
            _productsStore.FilterFunc = null;
            await _productsStore.SetToDefault();
        }

        public async Task SetBookStatisticFilterAsync(int periodFilterID)
        {
            _statisticPeriodFilterID = periodFilterID;
            await MergeAsync();
        }

        private async Task MergeAsync()
        {
            await _productsStore.SetToDefault();

            if (_statisticPeriodFilterID > 0)
            {

                _sales = await GetAllSalesRawAsync();

                var salesGroup = _sales.Where(s => _statisticOperationDelegates[_statisticPeriodFilterID - 1].Invoke(s)).GroupBy(a => a.Product.Id).Select(group => new
                {
                    BookId = group.Key,
                    TotalSales = group.Sum(s => s.Amount)
                }).OrderBy(a => a.TotalSales).Reverse();

                List<Product> tempProducts = new List<Product>();

                foreach (var sale in salesGroup)
                {
                    Product? tempProduct = await _productsStore.FindElementAsync(sale.BookId);
                    if (tempProduct != null)
                    {
                        tempProducts.Add(tempProduct);
                    }
                }
                await _productsStore.ChangeProducts(tempProducts.DistinctBy(a => a.Id).ToList());
            }
        }

        private async Task<IEnumerable<Sale>> GetAllSalesRawAsync()
        {
            return await Task.Run(() => _bookStoreContext.Sales.Include(a => a.Product).ThenInclude(a => a.Book).ThenInclude(b => b.Author).
                Include(a => a.Product).ThenInclude(a => a.Book).ThenInclude(b => b.Producer).
                Include(a => a.Product).ThenInclude(a => a.Book).ThenInclude(b => b.Genre).
                Include(a => a.Product).ThenInclude(a => a.DelayedForCustomer).ThenInclude(b => b.Customer).ThenInclude(c => c.FullName).
                Include(a => a.Product).ThenInclude(a => a.Discount));
        }
    }
}