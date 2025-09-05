using BLL.DTOEntityMappers;
using BLL.DTOs;
using DLL.Entities;
using DLL.Repositories.AccountSettingsRepositories;

namespace BLL.Services.AccountSettingsServices
{
    public class AccountSettingsService : IAccountSettingsService
    {
        private readonly IAccountSettingsRepository _accountSettingsRepository;

        public AccountSettingsService(IAccountSettingsRepository accountSettingsRepository)
        {
            _accountSettingsRepository = accountSettingsRepository;
        }

        public async Task<UserDTO> SignIn(UserSignInInfoDTO userSignInInfoDTO)
        {
            return UserMapper.ToDTO(await _accountSettingsRepository.SignIn(UserSignInMapper.ToEntity(userSignInInfoDTO)));
        }

        public async Task<UserDTO> SignUp(UserDTO userDTO)
        {
            return UserMapper.ToDTO(await _accountSettingsRepository.SignUp(UserMapper.ToEntity(userDTO)));
        }

        public async Task<UserDTO> ChangeLogin(UserDTO user, string newLogin)
        {
            return UserMapper.ToDTO(await _accountSettingsRepository.ChangeLogin(UserMapper.ToEntity(user), newLogin));
        }

        public async Task<UserDTO> ChangePassword(UserDTO user, string inputPassword, string newPassword)
        {
            return UserMapper.ToDTO(await _accountSettingsRepository.ChangePassword(UserMapper.ToEntity(user), inputPassword, newPassword));
        }
    }
}