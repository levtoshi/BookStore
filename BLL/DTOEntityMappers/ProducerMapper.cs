using BLL.DTOs;
using DLL.Entities;

namespace BLL.DTOEntityMappers
{
    public static class ProducerMapper
    {
        public static Producer ToEntity(ProducerDTO producerDTO)
        {
            return new Producer()
            {
                Id = producerDTO.Id,
                Name = producerDTO.Name
            };
        }

        public static ProducerDTO ToDTO(Producer producer)
        {
            return new ProducerDTO()
            {
                Id = producer.Id,
                Name = producer.Name
            };
        }
    }
}