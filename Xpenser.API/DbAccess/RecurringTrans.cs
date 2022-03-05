using Dapper;
using System.Collections.Generic;
using System.Data;
using Xpenser.API.DaCore;
using Xpenser.Models;

namespace Xpenser.API.DbAccess
{
    public class RecurringTransactionRepo : GenericRepository<ReccuringTransaction>, IReccuringTransactionRepository
    {
        public RecurringTransactionRepo(string connectionString) : base(connectionString) { }
        public override IEnumerable<ReccuringTransaction> GetAllById(long aSingleId)
        { throw new System.NotImplementedException(); }
        public override IEnumerable<ReccuringTransaction> GetAll()
        {
            using var vConn = GetOpenConnection();
            return vConn.Query<ReccuringTransaction>("ReccuringTransactionSelectAll", commandType: CommandType.StoredProcedure);
        }

        public override ReccuringTransaction GetIntSingle(int aSingleId)
        {
            throw new System.NotImplementedException();
        }

        public override ReccuringTransaction GetSingle(long aSingleId)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pReccuringTransactionId", aSingleId);
            return vConn.QueryFirstOrDefault<ReccuringTransaction>("ReccuringTransactionSelect", vParams, commandType: CommandType.StoredProcedure);
        }
        public override void Insert(ReccuringTransaction aEntity)
        {
            throw new System.NotImplementedException();
        }
        public override long InsertToGetId(ReccuringTransaction aEntity)
        {
            long lLastInsertedId = 0;
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pTransName", aEntity.TransName);
            vParams.Add("@pTransDescription", aEntity.TransDescription);
            vParams.Add("@pAmount", aEntity.Amount);
            vParams.Add("@pTransType", aEntity.TransType);
            vParams.Add("@pOccurance", aEntity.Occurance);
            vParams.Add("@pDayOfMonth", aEntity.DayOfMonth);
            vParams.Add("@pAppUserId", aEntity.AppUserId);
            vParams.Add("@pInsertedId", lLastInsertedId, direction: ParameterDirection.Output);
            vConn.Execute("ReccuringTransactionInsert4Id", vParams, commandType: CommandType.StoredProcedure);
            lLastInsertedId = vParams.Get<long>("@pInsertedId");
            return lLastInsertedId;
        }
        public override void Update(ReccuringTransaction aEntity)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pReccurTransId", aEntity.ReccurTransId);
            vParams.Add("@pTransName", aEntity.TransName);
            vParams.Add("@pTransDescription", aEntity.TransDescription);
            vParams.Add("@pAmount", aEntity.Amount);
            vParams.Add("@pTransType", aEntity.TransType);
            vParams.Add("@pOccurance", aEntity.Occurance);
            vParams.Add("@pDayOfMonth", aEntity.DayOfMonth);
            vParams.Add("@pAppUserId", aEntity.AppUserId);
            vConn.Execute("ReccuringTransactionUpdate", vParams, commandType: CommandType.StoredProcedure);
        }
    }
}
