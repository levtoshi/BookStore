using BLL.DTOs;
using DLL.Entities;
using DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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