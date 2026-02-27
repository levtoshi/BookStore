using BLL.DTOEntityMappers;
using BLL.DTOs;
using DLL.Repositories.BookDiscountRepositories;

namespace BLL.Services.BookDiscountServices
{
    public class BookDiscountService : IBookDiscountService
    {
        private readonly IBookDiscountRepository _bookDiscountRepository;

        public BookDiscountService(IBookDiscountRepository bookDiscountRepository)
        {
            _bookDiscountRepository = bookDiscountRepository;
        }

        public async Task<List<ProductDTO>> GetAllDiscountsAsync()
        {
            return (await _bookDiscountRepository.GetAllDiscountsAsync())
                .Select(d => ProductMapper.ToDTO(d))
                .ToList();
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