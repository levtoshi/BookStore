using BLL.DTOs;
using BookStoreUI.ViewModels.CollectionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreUI.ViewModelDTOMappers
{
    public static class UserMapper
    {
        public static UserViewModel ToViewModel(UserDTO userDTO)
        {
            return new UserViewModel
            {
                UserId = userDTO.Id,
                IsAdmin = userDTO.IsAdmin,
                Login = userDTO.SignInInfo.Login,
                Password = userDTO.SignInInfo.Password,
                Name = userDTO.UserFullName.Name,
                MiddleName = userDTO.UserFullName.MiddleName,
                LastName = userDTO.UserFullName.LastName
            };
        }

        public static UserDTO ToDTO(UserViewModel userViewModel)
        {
            return new UserDTO
            {
                Id = userViewModel.UserId,
                IsAdmin = userViewModel.IsAdmin,
                SignInInfo = new UserSignInInfoDTO()
                {
                    Login = userViewModel.Login,
                    Password = userViewModel.Password
                },
                UserFullName = new FullNameDTO()
                {
                    Name = userViewModel.Name,
                    MiddleName= userViewModel.MiddleName,
                    LastName= userViewModel.LastName
                }
            };
        }
    }
}