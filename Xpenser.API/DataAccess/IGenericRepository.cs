using System.Collections.Generic;
using System.Data;

namespace Xpenser.API.DataAccess
{
    public interface IGenericRepository<TEntity>
    {
        IDbConnection GetOpenConnection();
        void Insert(TEntity aEntity);
        void Update(TEntity aEntityToUpdate);
        TEntity GetSingle(long aSingleId);
        TEntity GetIntSingle(int aSingleId);
        IEnumerable<TEntity> GetAll();
    }
}
