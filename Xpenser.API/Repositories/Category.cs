using System.Collections.Generic;
using System.Data;
using Dapper;
using Xpenser.API.DataAccess;
using Xpenser.Models;

namespace Xpenser.API.Repositories
{
    public class CategoryRepo : MariaDbRepository<Category>, ICategoryRepository
    {
        public CategoryRepo(string connectionString) : base(connectionString) { }

        public override IEnumerable<Category> GetAll()
        {
            using var vConn = GetOpenConnection();
            return vConn.Query<Category>("CategorySelectAll", commandType: CommandType.StoredProcedure);
        }

        public override Category GetIntSingle(int aSingleId)
        {
            throw new System.NotImplementedException();
        }

        public override Category GetSingle(long aSingleId)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pCategoryId", aSingleId);
            return vConn.QueryFirstOrDefault<Category>("CategorySelect", vParams, commandType: CommandType.StoredProcedure);
        }
        public override void Insert(Category aEntity)
        {
            throw new System.NotImplementedException();
        }
        public override long InsertToGetId(Category aEntity)
        {
            long lLastInsertedId = 0;
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pCategoryName", aEntity.CategoryName);
            vParams.Add("@pCategoryDescription", aEntity.CategoryDescription);
            vParams.Add("@pParentId", aEntity.ParentId);
            vParams.Add("@pAppUserId", aEntity.AppUserId);
            vParams.Add("@pIconPicId", aEntity.IconPicId);
            vParams.Add("@pInsertedId", lLastInsertedId, direction: ParameterDirection.Output);
            vConn.Execute("CategoryInsert4Id", vParams, commandType: CommandType.StoredProcedure);
            lLastInsertedId = vParams.Get<long>("@pInsertedId");
            return lLastInsertedId;
        }
        public override void Update(Category aEntity)
        {
            using var vConn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@pCategoryId", aEntity.CategoryId);
            vParams.Add("@pCategoryName", aEntity.CategoryName);
            vParams.Add("@pCategoryDescription", aEntity.CategoryDescription);
            vParams.Add("@pParentId", aEntity.ParentId);
            vParams.Add("@pAppUserId", aEntity.AppUserId);
            vParams.Add("@pIconPicId", aEntity.IconPicId);
            vConn.Execute("CategoryUpdate", vParams, commandType: CommandType.StoredProcedure);
        }
    }
}
