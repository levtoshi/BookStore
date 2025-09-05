using BLL.DTOs;
using DLL.Entities;

namespace BLL.DTOEntityMappers
{
    public static class UserMapper
    {
        public static User ToEntity(UserDTO userDTO)
        {
            return new User()
            {
                Id = userDTO.Id,
                IsAdmin = userDTO.IsAdmin,
                SignInInfo = UserSignInMapper.ToEntity(userDTO.SignInInfo),
                UserFullName = FullNameMapper.ToEntity(userDTO.UserFullName)
            };
        }

        public static UserDTO ToDTO(User user)
        {
            return new UserDTO()
            {
                Id = user.Id,
                IsAdmin = user.IsAdmin,
                SignInInfo = UserSignInMapper.ToDTO(user.SignInInfo),
                UserFullName = FullNameMapper.ToDTO(user.UserFullName)
            };
        }
    }
}