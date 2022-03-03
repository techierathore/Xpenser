using System.Collections.Generic;
using System.Data;
using Dapper;
using TrDataAccess;
using Xpenser.Models;

namespace Xpenser.DbAccess
{
    public class UserLoginRepo : GenericRepository<UserLogin>, IUserLoginRepository
    {
        public UserLoginRepo(string connectionString) : base(connectionString) { }
        public override IEnumerable<UserLogin> GetAllById(long aSingleId)
        { throw new System.NotImplementedException(); }
        public override IEnumerable<UserLogin> GetAll()
        {
            using var vConn = GetOpenConnection();
            return vConn.Query<UserLogin>("UserLoginsSelectAll", commandType: CommandType.StoredProcedure);
        }

        public override UserLogin GetIntSingle(int aSingleId)
        {
            throw new System.NotImplementedException();
        }
        public UserLogin GetUserByToken(long aUserId, string aToken)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pUserId", aUserId);
            vParams.Add("@pLoginToken", aToken);
            return vConn.QueryFirstOrDefault<UserLogin>("GetUserToken", vParams, commandType: CommandType.StoredProcedure);
        }

        public override UserLogin GetSingle(long aSingleId)
        {
            throw new System.NotImplementedException();
        }
        public override long InsertToGetId(UserLogin aEntity)
        { throw new System.NotImplementedException(); }
        public override void Insert(UserLogin aEntity)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pUserId", aEntity.UserId);
            vParams.Add("@pLoginDate", aEntity.LoginDate);
            vParams.Add("@pLoginToken", aEntity.LoginToken);
            vParams.Add("@pTokenStatus", aEntity.TokenStatus);
            vParams.Add("@pExipryDate", aEntity.ExipryDate);
            vParams.Add("@pIssueDate", aEntity.IssueDate);
            int iResult = vConn.Execute("UserLoginsInsert", vParams, commandType: CommandType.StoredProcedure);
        }

        public override void Update(UserLogin aEntityToUpdate)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@LoginId", aEntityToUpdate.LoginId);
            vParams.Add("@LoginDate", aEntityToUpdate.LoginDate);
            vParams.Add("@LoginToken", aEntityToUpdate.LoginToken);
            vParams.Add("@TokenStatus", aEntityToUpdate.TokenStatus);
            vParams.Add("@ExipryDate", aEntityToUpdate.ExipryDate);
            vParams.Add("@IssueDate", aEntityToUpdate.IssueDate);
            vConn.Execute("UserLoginsUpdate", vParams, commandType: CommandType.StoredProcedure);
        }
    }
}
