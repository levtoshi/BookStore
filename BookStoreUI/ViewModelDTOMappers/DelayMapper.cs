using BLL.DTOs;
using BookStoreUI.ViewModels.CollectionViewModels;

namespace BookStoreUI.ViewModelDTOMappers
{
    public static class DelayMapper
    {
        public static DelayViewModel ToViewModel(ProductDTO productDTO)
        {
            return new DelayViewModel
            {
                ProductId = productDTO.Id,
                Name = productDTO.Book.Name,
                AuthorFullName = $"{productDTO.Book.Author.Name} {productDTO.Book.Author.MiddleName} {productDTO.Book.Author.LastName}",
                Year = productDTO.Book.Year,
                CustomerFullName = $"{productDTO.DelayedForCustomer.Customer.FullName.Name} {productDTO.DelayedForCustomer.Customer.FullName.MiddleName} {productDTO.DelayedForCustomer.Customer.FullName.LastName}",
                CustomerEmail = productDTO.DelayedForCustomer.Customer.Email,
                AmountOfBooksDelayed = productDTO.DelayedForCustomer.Amount,
                TotalPrice = productDTO.DelayedForCustomer.Amount * productDTO.Price
            };
        }
    }
}