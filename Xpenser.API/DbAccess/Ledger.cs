using Dapper;
using System.Collections.Generic;
using System.Data;
using Xpenser.API.DaCore;
using Xpenser.Models;

namespace Xpenser.API.DbAccess
{
    public class LedgerRepo : GenericRepository<Ledger>, ILedgerRepository
    {
        public LedgerRepo(string connectionString) : base(connectionString) { }
        public override IEnumerable<Ledger> GetAllById(long aSingleId)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pAppUserId", aSingleId);
            return vConn.Query<Ledger>("LedgerByUserId", vParams, commandType: CommandType.StoredProcedure);

        }

        public override IEnumerable<Ledger> GetAll()
        {
            using var vConn = GetOpenConnection();
            return vConn.Query<Ledger>("LedgerSelectAll", commandType: CommandType.StoredProcedure);
        }

        public override Ledger GetIntSingle(int aSingleId)
        {
            throw new System.NotImplementedException();
        }

        public override Ledger GetSingle(long aSingleId)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pTransId", aSingleId);
            return vConn.QueryFirstOrDefault<Ledger>("LedgerSelect", vParams, commandType: CommandType.StoredProcedure);
        }
        public override void Insert(Ledger aEntity)
        {
            throw new System.NotImplementedException();
        }
        public override long InsertToGetId(Ledger aEntity)
        {
            long lLastInsertedId = 0;
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pTransName", aEntity.TransName);
            vParams.Add("@pTransDescription", aEntity.TransDescription);
            vParams.Add("@pAmount", aEntity.Amount);
            vParams.Add("@pTransType", aEntity.TransType);
            vParams.Add("@pAppUserId", aEntity.AppUserId);
            vParams.Add("@pCategoryId", aEntity.CategoryId);
            vParams.Add("@pSrcAccId", aEntity.SrcAccId);
            vParams.Add("@pDestAccId", aEntity.DestAccId);
            vParams.Add("@pPicIds", aEntity.PicIds);
            vParams.Add("@pInsertedId", lLastInsertedId, direction: ParameterDirection.Output);
            vConn.Execute("LedgerInsert", vParams, commandType: CommandType.StoredProcedure);
            lLastInsertedId = vParams.Get<long>("@pInsertedId");
            return lLastInsertedId;
        }
        public override void Update(Ledger aEntity)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pTransId", aEntity.TransId);
            vParams.Add("@pTransName", aEntity.TransName);
            vParams.Add("@pTransDescription", aEntity.TransDescription);
            vParams.Add("@pAmount", aEntity.Amount);
            vParams.Add("@pTransType", aEntity.TransType);
            vParams.Add("@pAppUserId", aEntity.AppUserId);
            vParams.Add("@pCategoryId", aEntity.CategoryId);
            vParams.Add("@pSrcAccId", aEntity.SrcAccId);
            vParams.Add("@pDestAccId", aEntity.DestAccId);
            vParams.Add("@pPicIds", aEntity.PicIds);
            vConn.Execute("LedgerUpdate", vParams, commandType: CommandType.StoredProcedure);
        }
    }
}
