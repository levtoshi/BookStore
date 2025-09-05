using DLL.Entities;

namespace DLL.Repositories.AccountSettingsRepositories
{
    public interface IAccountSettingsRepository
    {
        Task<User> SignIn(UserSignInInfo userSignInInfo);
        Task<User> SignUp(User user);
        Task<User> ChangeLogin(User user, string newLogin);
        Task<User> ChangePassword(User user, string inputPassword, string newPassword);
    }
}