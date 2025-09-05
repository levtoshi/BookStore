using BLL.DTOs;
using BookStoreUI.ViewModels.CollectionViewModels;

namespace BookStoreUI.ViewModelDTOMappers
{
    public static class ProductMapper
    {
        public static ProductViewModel ToViewModel(ProductDTO productDTO)
        {
            return new ProductViewModel
            {
                ProductId = productDTO.Id,
                Name = productDTO.Book.Name,
                AuthorFullName = $"{productDTO.Book.Author.Name} {productDTO.Book.Author.MiddleName} {productDTO.Book.Author.LastName}",
                ProducerName = productDTO.Book.Producer.Name,
                PagesAmount = productDTO.Book.PageAmount,
                Genre = productDTO.Book.Genre.Name,
                Year = productDTO.Book.Year,
                IsContinuation = (productDTO.Book.IsContinuation == null) ? "No" : productDTO.Book.IsContinuation,
                AmountInStock = productDTO.Amount,
                Cost = productDTO.Cost,
                Price = productDTO.Price
            };
        }

        public static ProductDTO ToDTO(ProductViewModel productViewModel)
        {
            return new ProductDTO()
            {
                Id = productViewModel.ProductId,
                Amount = productViewModel.AmountInStock,
                Cost = productViewModel.Cost,
                Price = productViewModel.Price,
                Book = new BookDTO()
                {
                    Name = productViewModel.Name,
                    Author = new FullNameDTO()
                    {
                        Name = productViewModel.AuthorFullName.Split(' ')[0],
                        MiddleName = productViewModel.AuthorFullName.Split(' ')[1],
                        LastName = productViewModel.AuthorFullName.Split(' ')[2]
                    },
                    Producer = new ProducerDTO()
                    {
                        Name = productViewModel.ProducerName
                    },
                    PageAmount = productViewModel.PagesAmount,
                    Genre = new GenreDTO()
                    {
                        Name = productViewModel.Genre
                    },
                    Year = productViewModel.Year,
                    IsContinuation = (productViewModel.IsContinuation == "No") ? null : productViewModel.IsContinuation
                }
            };
        }
    }
}