using BLL.DTOs;
using DLL.Entities;

namespace BLL.DTOEntityMappers
{
    public static class DelayMapper
    {
        public static Delay ToEntity(DelayDTO delayDTO)
        {
            return new Delay()
            {
                Id = delayDTO.Id,
                Customer = CustomerMapper.ToEntity(delayDTO.Customer),
                Amount = delayDTO.Amount
            };
        }

        public static DelayDTO ToDTO(Delay delay)
        {
            return new DelayDTO()
            {
                Id = delay.Id,
                Customer = CustomerMapper.ToDTO(delay.Customer),
                Amount = delay.Amount
            };
        }
    }
}