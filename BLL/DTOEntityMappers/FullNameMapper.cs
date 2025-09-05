using BLL.DTOs;
using DLL.Entities;

namespace BLL.DTOEntityMappers
{
    public static class FullNameMapper
    {
        public static FullName ToEntity(FullNameDTO fullNameDTO)
        {
            return new FullName()
            {
                Id = fullNameDTO.Id,
                Name = fullNameDTO.Name,
                MiddleName = fullNameDTO.MiddleName,
                LastName = fullNameDTO.LastName
            };
        }

        public static FullNameDTO ToDTO(FullName fullName)
        {
            return new FullNameDTO()
            {
                Id = fullName.Id,
                Name = fullName.Name,
                MiddleName = fullName.MiddleName,
                LastName = fullName.LastName
            };
        }
    }
}