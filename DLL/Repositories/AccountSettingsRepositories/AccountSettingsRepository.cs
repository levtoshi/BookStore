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
            User? tempUser = await FindUserBySignInInfo(userSignInInfo);

            if (tempUser != null)
            {
                return tempUser;
            }
            else
            {
                throw new Exception("User not found!");
            }
        }

        public async Task<User> SignUp(User user)
        {
            IEnumerable<User> tempUsers = await GetAllAsync();

            if (tempUsers.Where(u => u.SignInInfo.Login == user.SignInInfo.Login).Count() == 0)
            {
                await _bookStoreContext.Users.AddAsync(user);
                await _bookStoreContext.SaveChangesAsync();

                return await FindUserBySignInInfo(user.SignInInfo);
            }
            else
            {
                throw new Exception("This login already exists!");
            }
        }

        public async Task<User> ChangeLogin(User user, string newLogin)
        {
            User? tempUser = await FindUserBySignInInfo(user.SignInInfo);

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
            User? tempUser = await FindUserBySignInInfo(user.SignInInfo);

            if (tempUser != null)
            {
                if (tempUser.SignInInfo.Password == inputPassword)
                {
                    tempUser.SignInInfo.Password = newPassword;
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

        private async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Task.Run(() => _bookStoreContext.Users
                .Include(u => u.SignInInfo)
                .Include(u => u.UserFullName));
        }

        private async Task<User?> FindUserBySignInInfo(UserSignInInfo userSignInInfo)
        {
            return await _bookStoreContext.Users
                .Include(u => u.SignInInfo)
                .Include(u => u.UserFullName)
                .FirstOrDefaultAsync(u => u.SignInInfo.Login == userSignInInfo.Login && u.SignInInfo.Password == userSignInInfo.Password);
        }
    }
}