

using Xpenser.API.DataAccess;
using Xpenser.Models;

namespace Xpenser.API.Repositories
{
    public interface IAppUserRepository : IGenericRepository<AppUser>
    {
        AppUser GetLoginUser(string aLoginEmail, string aPassword);
        AppUser GetUserByEmail(string loginEmail);
        AppUser GetUserByMobile(string aMobileNo);
    }
    public interface IUserLoginRepository : IGenericRepository<UserLogin>
    {
        UserLogin GetUserByToken(long aUserId, string aToken);
    }
    public interface IAccountRepository : IGenericRepository<Account>
    { }
    public interface ICategoryRepository : IGenericRepository<Category>
    { }
    public interface IReccuringTransactionRepository : IGenericRepository<ReccuringTransaction>
    { }
}
