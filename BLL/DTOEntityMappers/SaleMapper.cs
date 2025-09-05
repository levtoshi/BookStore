using BLL.DTOs;
using DLL.Entities;

namespace BLL.DTOEntityMappers
{
    public static class SaleMapper
    {
        public static Sale ToEntity(SaleDTO saleDTO)
        {
            return new Sale()
            {
                Id = saleDTO.Id,
                Amount = saleDTO.Amount,
                SoldTime = saleDTO.SoldTime,
                Product = ProductMapper.ToEntity(saleDTO.Product)
            };
        }

        public static SaleDTO ToDTO(Sale sale)
        {
            return new SaleDTO()
            {
                Id = sale.Id,
                Amount = sale.Amount,
                SoldTime = sale.SoldTime,
                Product = ProductMapper.ToDTO(sale.Product)
            };
        }
    }
}