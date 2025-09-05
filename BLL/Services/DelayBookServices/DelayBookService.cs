using BLL.DTOEntityMappers;
using BLL.DTOs;
using BLL.Interfaces;
using DLL.Repositories.DelayBookRepositories;

namespace BLL.Services.DelayBookServices
{
    public class DelayBookService : IDelayBookService, ISetCollectionToDefaultService
    {
        private readonly IDelayBookRepository _delayBookRepository;

        public DelayBookService(IDelayBookRepository delayBookRepository)
        {
            _delayBookRepository = delayBookRepository;
        }

        public async Task SetToDefault()
        {
            await _delayBookRepository.SetToDefault();
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