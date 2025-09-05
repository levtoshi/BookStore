using BLL.DTOEntityMappers;
using BLL.DTOs;
using BLL.Interfaces;
using DLL.Repositories.BookDiscountRepositories;

namespace BLL.Services.BookDiscountServices
{
    public class BookDiscountService : IBookDiscountService, ISetCollectionToDefaultService
    {
        private readonly IBookDiscountRepository _bookDiscountRepository;

        public BookDiscountService(IBookDiscountRepository bookDiscountRepository)
        {
            _bookDiscountRepository = bookDiscountRepository;
        }

        public async Task SetToDefault()
        {
            await _bookDiscountRepository.SetToDefault();
        }

        public async Task AddDiscountAsync(int productId, DiscountDTO discount)
        {
            await _bookDiscountRepository.AddDiscountAsync(productId, DiscountMapper.ToEntity(discount));
        }

        public async Task RemoveDiscountAsync(int productId)
        {
            await _bookDiscountRepository.RemoveDiscountAsync(productId);
        }
    }
}