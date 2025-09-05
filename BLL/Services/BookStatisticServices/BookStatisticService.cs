using BLL.Interfaces;
using DLL.Repositories.BookStatisticRepositories;

namespace BLL.Services.BookStatisticServices
{
    public class BookStatisticService : IBookStatisticService, ISetCollectionToDefaultService
    {
        private readonly IBookStatisticRepository _bookStatisticRepository;

        public BookStatisticService(IBookStatisticRepository bookStatisticRepository)
        {
            _bookStatisticRepository = bookStatisticRepository;
        }

        public async Task SetToDefault()
        {
            await _bookStatisticRepository.SetToDefault();
        }

        public async Task SetBookStatisticFilterAsync(int periodFilterID)
        {
            await _bookStatisticRepository.SetBookStatisticFilterAsync(periodFilterID);
        }
    }
}