using BLL.DTOs;
using DLL.Entities;

namespace BLL.DTOEntityMappers
{
    public static class DiscountMapper
    {
        public static Discount ToEntity(DiscountDTO discountDTO)
        {
            return new Discount()
            {
                Id = discountDTO.Id,
                Name = discountDTO.Name,
                Interest = discountDTO.Interest,
                StartDate = discountDTO.StartDate,
                EndDate = discountDTO.EndDate
            };
        }

        public static DiscountDTO ToDTO(Discount discount)
        {
            return new DiscountDTO()
            {
                Id = discount.Id,
                Name = discount.Name,
                Interest = discount.Interest,
                StartDate = discount.StartDate,
                EndDate = discount.EndDate
            };
        }
    }
}