using System.Collections.Generic;
using System.Data;
using Dapper;
using Xpenser.API.DataAccess;
using Xpenser.Models;

namespace Xpenser.API.Repositories
{
    public class AccountRepo : MariaDbRepository<Account>, IAccountRepository
    {
        public AccountRepo(string connectionString) : base(connectionString) { }

        public override IEnumerable<Account> GetAll()
        {
            using var vConn = GetOpenConnection();
            return vConn.Query<Account>("AccountSelectAll", commandType: CommandType.StoredProcedure);
        }

        public override Account GetIntSingle(int aSingleId)
        {
            throw new System.NotImplementedException();
        }

        public override Account GetSingle(long aSingleId)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pAccountId", aSingleId);
            return vConn.QueryFirstOrDefault<Account>("AccountSelect", vParams, commandType: CommandType.StoredProcedure);
        }
        public override void Insert(Account aEntity)
        {
            throw new System.NotImplementedException();
        }
        public override long InsertToGetId(Account aEntity)
        {
            long lLastInsertedId = 0;
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pAcccountName", aEntity.AcccountName);
            vParams.Add("@pAcNumber", aEntity.AcNumber);
            vParams.Add("@pOpenBal", aEntity.OpenBal);
            vParams.Add("@pAcType", aEntity.AcType);
            vParams.Add("@pStartDate", aEntity.StartDate);
            vParams.Add("@pAppUserId", aEntity.AppUserId);
            vParams.Add("@pIconPicId", aEntity.IconPicId);
            vParams.Add("@pInsertedId", lLastInsertedId, direction: ParameterDirection.Output);
            vConn.Execute("AccountInsert4Id", vParams, commandType: CommandType.StoredProcedure);
            lLastInsertedId = vParams.Get<long>("@pInsertedId");
            return lLastInsertedId;
        }
        public override void Update(Account aEntity)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pAccountId", aEntity.AccountId);
            vParams.Add("@pAcccountName", aEntity.AcccountName);
            vParams.Add("@pAcNumber", aEntity.AcNumber);
            vParams.Add("@pOpenBal", aEntity.OpenBal);
            vParams.Add("@pAcType", aEntity.AcType);
            vParams.Add("@pStartDate", aEntity.StartDate);
            vParams.Add("@pAppUserId", aEntity.AppUserId);
            vParams.Add("@pIconPicId", aEntity.IconPicId);
            vConn.Execute("AccountUpdate", vParams, commandType: CommandType.StoredProcedure);
        }
    }
}
