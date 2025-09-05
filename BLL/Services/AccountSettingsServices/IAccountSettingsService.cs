using BLL.DTOs;
using DLL.Entities;

namespace BLL.Services.AccountSettingsServices
{
    public interface IAccountSettingsService
    {
        Task<UserDTO> SignIn(UserSignInInfoDTO userSignInInfoDTO);
        Task<UserDTO> SignUp(UserDTO userDTO);
        Task<UserDTO> ChangeLogin(UserDTO user, string newLogin);
        Task<UserDTO> ChangePassword(UserDTO user, string inputPassword, string newPassword);
    }
}