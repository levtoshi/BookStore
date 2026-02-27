using BLL.DTOEntityMappers;
using BLL.DTOs;
using DLL.Entities;
using DLL.Models;
using DLL.Repositories.FilterBookRepositories;

namespace BLL.Services.FilterBookServices
{
    public class FilterBookService : IFilterBookService
    {
        private readonly IFilterBookRepository _filterBookRepository;

        public FilterBookService(IFilterBookRepository filterBookRepository)
        {
            _filterBookRepository = filterBookRepository;
        }

        public async Task<List<ProductDTO>> GetBooksByFilterAsync(FilterInfoDTO filterInfoDTO)
        {
            return (await _filterBookRepository.GetBooksByFilterAsync(FilterInfoMapper.ToEntity(filterInfoDTO)))
                .Select(p => ProductMapper.ToDTO(p))
                .ToList();
        }
    }
}