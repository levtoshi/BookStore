using BLL.DTOs;
using DLL.Entities;

namespace BLL.DTOEntityMappers
{
    public static class BookMapper
    {
        public static Book ToEntity(BookDTO bookDTO)
        {
            return new Book()
            {
                Id = bookDTO.Id,
                Name = bookDTO.Name,
                Author = FullNameMapper.ToEntity(bookDTO.Author),
                Producer = ProducerMapper.ToEntity(bookDTO.Producer),
                PageAmount = bookDTO.PageAmount,
                Genre = GenreMapper.ToEntity(bookDTO.Genre),
                Year = bookDTO.Year,
                IsContinuation = bookDTO.IsContinuation
            };
        }

        public static BookDTO ToDTO(Book book)
        {
            return new BookDTO()
            {
                Id = book.Id,
                Name = book.Name,
                Author = FullNameMapper.ToDTO(book.Author),
                Producer = ProducerMapper.ToDTO(book.Producer),
                PageAmount = book.PageAmount,
                Genre = GenreMapper.ToDTO(book.Genre),
                Year = book.Year,
                IsContinuation = book.IsContinuation
            };
        }
    }
}