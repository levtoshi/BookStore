using BLL.DTOEntityMappers;
using BLL.DTOs;
using BLL.Interfaces;
using DLL.Repositories.BookModelRepositories;

namespace BLL.Services.BookModelServices
{
    public class BookModelService : IBookModelService, ISetCollectionToDefaultService
    {
        private readonly IBookModelRepository _bookModelRepository;

        public BookModelService(IBookModelRepository bookModelRepository)
        {
            _bookModelRepository = bookModelRepository;
        }

        public async Task SetToDefault()
        {
            await _bookModelRepository.SetToDefault();
        }

        public async Task AddBookModelAsync(ProductDTO productDTO)
        {
            await _bookModelRepository.AddBookModelAsync(ProductMapper.ToEntity(productDTO));
        }

        public async Task UpdateBookModelAsync(ProductDTO productDTO)
        {
            await _bookModelRepository.UpdateBookModelAsync(ProductMapper.ToEntity(productDTO));
        }

        public async Task DeleteBookModelAsync(ProductDTO productDTO)
        {
            await _bookModelRepository.DeleteBookModelAsync(ProductMapper.ToEntity(productDTO));
        }
    }
}