﻿using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xpenser.API.DaCore;
using Xpenser.Models;
namespace Xpenser.API.DbAccess
{
    public class AppUserRepo : GenericRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepo(string connectionString) : base(connectionString) { }

        public override IEnumerable<AppUser> GetAllById(long aSingleId)
        { throw new System.NotImplementedException(); }
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
            throw new System.NotImplementedException();
        }
        public override long InsertToGetId(AppUser aEntity)
        {
            long lLastInsertedId = 0;
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pFirstName", aEntity.FirstName);
            vParams.Add("@pLastName", aEntity.LastName);
            vParams.Add("@pEmailID", aEntity.EmailID);
            vParams.Add("@pPasswordHash", aEntity.PasswordHash);
            vParams.Add("@pMobileNo", aEntity.MobileNo);
            vParams.Add("@pIsVerified", aEntity.IsVerified);
            vParams.Add("@pRole", aEntity.Role);
            vParams.Add("@pInsertedId", lLastInsertedId, direction: ParameterDirection.Output);
            vConn.Execute("AppUserInsert", vParams, commandType: CommandType.StoredProcedure);
            lLastInsertedId = vParams.Get<long>("@pInsertedId");
            return lLastInsertedId;
        }
        public override void Update(AppUser aEntity)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pAppUserId", aEntity.AppUserId);
            vParams.Add("@pFirstName", aEntity.FirstName);
            vParams.Add("@pLastName", aEntity.LastName);
            vParams.Add("@pEmailID", aEntity.EmailID);
            vParams.Add("@pPasswordHash", aEntity.PasswordHash);
            vParams.Add("@pMobileNo", aEntity.MobileNo);
            vParams.Add("@pIsVerified", aEntity.IsVerified);
            vParams.Add("@pRole", aEntity.Role);
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
        public AppUser GetUserByEmail(string loginEmail)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pEmailID", loginEmail);
            return vConn.QueryFirstOrDefault<AppUser>("AppUserByEmail", vParams, commandType: CommandType.StoredProcedure);
        }
        public AppUser GetUserByMobile(string aMobileNo)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pMobileNo", aMobileNo);
            return vConn.QueryFirstOrDefault<AppUser>("AppUserByMobile", vParams, commandType: CommandType.StoredProcedure);
        }
    }
}
