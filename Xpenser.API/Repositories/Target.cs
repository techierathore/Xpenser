using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Xpenser.API.DataAccess;
using Xpenser.Models;

namespace Xpenser.API.Repositories
{
    public class TargetRepo : MariaDbRepository<Target>, ITargetRepository
    {
        public TargetRepo(string connectionString) : base(connectionString) { }

        public override IEnumerable<Target> GetAllById(long aSingleId)
        { throw new System.NotImplementedException(); }

        public override IEnumerable<Target> GetAll()
        {
            using var vConn = GetOpenConnection();
            return vConn.Query<Target>("TargetSelectAll", commandType: CommandType.StoredProcedure);
        }

        public override Target GetIntSingle(int aSingleId)
        {
            throw new System.NotImplementedException();
        }

        public override Target GetSingle(long aSingleId)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pTargetId", aSingleId);
            return vConn.QueryFirstOrDefault<Target>("TargetSelect", vParams, commandType: CommandType.StoredProcedure);
        }
        public override void Insert(Target aEntity)
        {
            long lLastInsertedId = 0;
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pTargetTitle", aEntity.TargetTitle);
            vParams.Add("@pTargetDescription", aEntity.TargetDescription);
            vParams.Add("@pCategoryId", aEntity.CategoryId);
            vParams.Add("@pEntryDate", aEntity.EntryDate);
            vParams.Add("@pTargetDate", aEntity.TargetDate);
            vParams.Add("@pAmount", aEntity.Amount);
            vParams.Add("@pAppUserId", aEntity.AppUserId);
            vConn.Execute("TargetInsert", vParams, commandType: CommandType.StoredProcedure);
        }
        public override long InsertToGetId(Target aEntity)
        {
            throw new System.NotImplementedException();
        }
        public override void Update(Target aEntity)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pTargetId", aEntity.TargetId);
            vParams.Add("@pTargetTitle", aEntity.TargetTitle);
            vParams.Add("@pTargetDescription", aEntity.TargetDescription);
            vParams.Add("@pCategoryId", aEntity.CategoryId);
            vParams.Add("@pEntryDate", aEntity.EntryDate);
            vParams.Add("@pTargetDate", aEntity.TargetDate);
            vParams.Add("@pAmount", aEntity.Amount);
            vParams.Add("@pAppUserId", aEntity.AppUserId);
            vConn.Execute("TargetUpdate", vParams, commandType: CommandType.StoredProcedure);

        }
    }
}
