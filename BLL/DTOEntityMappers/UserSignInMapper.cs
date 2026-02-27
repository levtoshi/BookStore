using BLL.DTOs;
using DLL.Entities;

namespace BLL.DTOEntityMappers
{
    public static class UserSignInMapper
    {
        public static UserSignInInfo ToEntity(UserSignInInfoDTO userSignInInfoDTO)
        {
            return new UserSignInInfo()
            {
                Id = userSignInInfoDTO.Id,
                Login = userSignInInfoDTO.Login,
                Password = userSignInInfoDTO.Password
            };
        }

        public static UserSignInInfoDTO ToDTO(UserSignInInfo userSignInInfo)
        {
            return new UserSignInInfoDTO()
            {
                Id = userSignInInfo.Id,
                Login = userSignInInfo.Login
            };
        }
    }
}