using BLL.DTOs;
using DLL.Entities;

namespace BLL.DTOEntityMappers
{
    public static class CustomerMapper
    {
        public static Customer ToEntity(CustomerDTO customerDTO)
        {
            return new Customer()
            {
                Id = customerDTO.Id,
                FullName = FullNameMapper.ToEntity(customerDTO.FullName),
                Email = customerDTO.Email
            };
        }

        public static CustomerDTO ToDTO(Customer customer)
        {
            return new CustomerDTO()
            {
                Id = customer.Id,
                FullName = FullNameMapper.ToDTO(customer.FullName),
                Email = customer.Email
            };
        }
    }
}