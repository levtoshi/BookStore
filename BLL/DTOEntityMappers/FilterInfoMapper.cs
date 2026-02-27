using BLL.DTOs;
using DLL.Models;

namespace BLL.DTOEntityMappers
{
    public class FilterInfoMapper
    {
        public static FilterInfo ToEntity(FilterInfoDTO filterInfoDTO)
        {
            return new FilterInfo()
            {
                Name = filterInfoDTO.Name,
                Author = filterInfoDTO.Author,
                Genre = filterInfoDTO.Genre,
                Order = filterInfoDTO.Order,
                Period = filterInfoDTO.Period
            };
        }

        public static FilterInfoDTO ToDTO(FilterInfo filterInfo)
        {
            return new FilterInfoDTO()
            {
                Name = filterInfo.Name,
                Author = filterInfo.Author,
                Genre = filterInfo.Genre,
                Order = filterInfo.Order,
                Period = filterInfo.Period
            };
        }
    }
}