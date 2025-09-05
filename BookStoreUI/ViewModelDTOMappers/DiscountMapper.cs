using BLL.DTOs;
using BookStoreUI.ViewModels.CollectionViewModels;

namespace BookStoreUI.ViewModelDTOMappers
{
    public static class DiscountMapper
    {
        public static DiscountViewModel ToViewModel(ProductDTO productDTO)
        {
            return new DiscountViewModel
            {
                ProductId = productDTO.Id,
                Name = productDTO.Book.Name,
                AuthorFullName = $"{productDTO.Book.Author.Name} {productDTO.Book.Author.MiddleName} {productDTO.Book.Author.LastName}",
                Year = productDTO.Book.Year,
                DiscountName = productDTO.Discount.Name,
                Interest = productDTO.Discount.Interest,
                StartDate = productDTO.Discount.StartDate,
                EndDate = productDTO.Discount.EndDate
            };
        }
    }
}