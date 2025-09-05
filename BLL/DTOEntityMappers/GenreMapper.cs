using BLL.DTOs;
using DLL.Entities;

namespace BLL.DTOEntityMappers
{
    public static class GenreMapper
    {
        public static Genre ToEntity(GenreDTO genreDTO)
        {
            return new Genre()
            {
                Id = genreDTO.Id,
                Name = genreDTO.Name
            };
        }

        public static GenreDTO ToDTO(Genre genre)
        {
            return new GenreDTO()
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }
    }
}