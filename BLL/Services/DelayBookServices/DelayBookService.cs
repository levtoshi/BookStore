using BLL.DTOEntityMappers;
using BLL.DTOs;
using DLL.Repositories.DelayBookRepositories;

namespace BLL.Services.DelayBookServices
{
    public class DelayBookService : IDelayBookService
    {
        private readonly IDelayBookRepository _delayBookRepository;

        public DelayBookService(IDelayBookRepository delayBookRepository)
        {
            _delayBookRepository = delayBookRepository;
        }

        public async Task<List<ProductDTO>> GetAllDelaysAsync()
        {
            return (await _delayBookRepository.GetAllDelaysAsync())
                .Select(d => ProductMapper.ToDTO(d))
                .ToList();
        }

        public async Task AddDelayAsync(int productId, DelayDTO delay)
        {
            await _delayBookRepository.AddDelayAsync(productId, DelayMapper.ToEntity(delay));
        }

        public async Task RemoveDelayAsync(int productId)
        {
            await _delayBookRepository.RemoveDelayAsync(productId);
        }
    }
}