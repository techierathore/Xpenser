using System.Collections.Generic;
using System.Data;
using Dapper;
using Xpenser.API.DataAccess;
using Xpenser.Models;

namespace Xpenser.API.Repositories
{
    public class AppUserRepo : MariaDbRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepo(string connectionString) : base(connectionString) { }

        public override IEnumerable<AppUser> GetAll()
        {
            using var vConn = GetOpenConnection();
            return vConn.Query<AppUser>("AppUserSelectAll", commandType: CommandType.StoredProcedure);
        }

        public override AppUser GetIntSingle(int aSingleId)
        {
            throw new System.NotImplementedException();
        }

        public override AppUser GetSingle(long aSingleId)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pAppUserId", aSingleId);
            return vConn.QueryFirstOrDefault<AppUser>("AppUserSelect", vParams, commandType: CommandType.StoredProcedure);
        }
        public override void Insert(AppUser aEntity)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pFirstName", aEntity.FirstName);
            vParams.Add("@pLastName", aEntity.LastName);
            vParams.Add("@pUserEmail", aEntity.EmailID);
            vParams.Add("@pLoginPass", aEntity.LoginPassword);
            vParams.Add("@pAppUserRole", aEntity.Role);
            int iResult = vConn.Execute("AppUserInsert", vParams, commandType: CommandType.StoredProcedure);
        }

        public override void Update(AppUser aEntityToUpdate)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pAppUserId", aEntityToUpdate.AppUserId);
            vParams.Add("@pFirstName", aEntityToUpdate.FirstName);
            vParams.Add("@pLastName", aEntityToUpdate.LastName);
            vParams.Add("@pUserEmail", aEntityToUpdate.EmailID);
            vParams.Add("@pLoginPass", aEntityToUpdate.LoginPassword);
            vParams.Add("@pAppUserRole", aEntityToUpdate.Role);
            vConn.Execute("AppUserUpdate", vParams, commandType: CommandType.StoredProcedure);
        }

        public AppUser GetLoginUser(string aLoginEmail, string aPassword)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pEmail", aLoginEmail);
            vParams.Add("@pPasswordHash", aPassword);
            var vReturnUser = vConn.Query<AppUser>("ValidateLogin", vParams, commandType: CommandType.StoredProcedure).SingleOrDefault();
            return vReturnUser;
        }
    }
}
