using Xpenser.Models;

namespace Xpenser.API.DaCore
{
    public interface IAppUserRepository : IGenericRepository<AppUser>
    {
        AppUser GetLoginUser(string aLoginEmail, string aPassword);
        AppUser GetUserByEmail(string loginEmail);
        AppUser GetUserByMobile(string aMobileNo);
        AppUser GetUserByVerificationCode(string aVerificationCode);
        void UpdateUserEmail(AppUser aUser);
        void UpdateVerificationCode(string verificationCode);
    }
    public interface IUserLoginRepository : IGenericRepository<UserLogin>
    {
        UserLogin GetUserByToken(long aUserId, string aToken);
    }
    public interface IAccountRepository : IGenericRepository<Account>
    { }
    public interface ICategoryRepository : IGenericRepository<Category>
    { }
    public interface ILedgerRepository : IGenericRepository<Ledger>
    { }
    public interface IReccuringTransactionRepository : IGenericRepository<ReccuringTransaction>
    { }

    public interface ITargetRepository : IGenericRepository<Target>
    { }
}
