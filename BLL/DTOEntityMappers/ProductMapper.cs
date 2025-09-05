using BLL.DTOs;
using DLL.Entities;

namespace BLL.DTOEntityMappers
{
    public static class ProductMapper
    {
        public static Product ToEntity(ProductDTO productDTO)
        {
            return new Product()
            {
                Id = productDTO.Id,
                Amount = productDTO.Amount,
                Cost = productDTO.Cost,
                Price = productDTO.Price,
                Book = BookMapper.ToEntity(productDTO.Book),
                Discount = productDTO.Discount != null ? DiscountMapper.ToEntity(productDTO.Discount) : null,
                DelayedForCustomer = productDTO.DelayedForCustomer != null ? DelayMapper.ToEntity(productDTO.DelayedForCustomer) : null
            };
        }

        public static ProductDTO ToDTO(Product product)
        {
            return new ProductDTO()
            {
                Id = product.Id,
                Amount = product.Amount,
                Cost = product.Cost,
                Price = product.Price,
                Book = BookMapper.ToDTO(product.Book),
                Discount = product.Discount != null ? DiscountMapper.ToDTO(product.Discount) : null,
                DelayedForCustomer = product.DelayedForCustomer != null ? DelayMapper.ToDTO(product.DelayedForCustomer) : null
            };
        }
    }
}