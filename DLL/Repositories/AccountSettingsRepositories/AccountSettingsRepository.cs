using DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories.AccountSettingsRepositories
{
    public class AccountSettingsRepository : IAccountSettingsRepository
    {
        private readonly BookStoreContext _bookStoreContext;

        public AccountSettingsRepository(BookStoreContext bookStoreContext)
        {
            _bookStoreContext = bookStoreContext;
        }

        public async Task<User> SignIn(UserSignInInfo userSignInInfo)
        {
            User? tempUser = await FindUserByLogin(userSignInInfo.Login);

            if (tempUser != null)
            {
                if (BCrypt.Net.BCrypt.Verify(userSignInInfo.Password, tempUser.SignInInfo.Password))
                {
                    return tempUser;
                }
                throw new Exception("Incorrect password!");
            }
            else
            {
                throw new Exception("User not found!");
            }
        }

        public async Task<User> SignUp(User user)
        {
            User? tempUser = await FindUserByLogin(user.SignInInfo.Login);

            if (tempUser == null)
            {
                user.SignInInfo.Password = BCrypt.Net.BCrypt.HashPassword(user.SignInInfo.Password);
                _bookStoreContext.Users.Add(user);
                await _bookStoreContext.SaveChangesAsync();
                return user;
            }
            else
            {
                throw new Exception("This login already exists!");
            }
        }

        public async Task<User> ChangeLogin(User user, string newLogin)
        {
            User? tempUser = await FindUserByLogin(user.SignInInfo.Login);

            if (tempUser != null)
            {
                tempUser.SignInInfo.Login = newLogin;
                await _bookStoreContext.SaveChangesAsync();
                return tempUser;
            }
            else
            {
                throw new Exception("Validation error!");
            }
        }

        public async Task<User> ChangePassword(User user, string inputPassword, string newPassword)
        {
            User? tempUser = await FindUserByLogin(user.SignInInfo.Login);

            if (tempUser != null)
            {
                if (BCrypt.Net.BCrypt.Verify(inputPassword, tempUser.SignInInfo.Password))
                {
                    tempUser.SignInInfo.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                    await _bookStoreContext.SaveChangesAsync();
                    return tempUser;
                }
                else
                {
                    throw new Exception("Incorrent password!");
                }
            }
            else
            {
                throw new Exception("Validation error!");
            }
        }

        private async Task<User?> FindUserByLogin(string login)
        {
            return await _bookStoreContext.Users
                .Include(u => u.SignInInfo)
                .Include(u => u.UserFullName)
                .FirstOrDefaultAsync(u => u.SignInInfo.Login == login);
        }
    }
}