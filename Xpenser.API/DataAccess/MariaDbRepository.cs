using System.Collections.Generic;
using System.Data;

namespace Xpenser.API.DataAccess
{
    /// <summary>
    /// The concrete implementation of a SQL repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class MariaDbRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        private string _connectionString;
        private EDbConnectionTypes _dbType;

        public MariaDbRepository(string connectionString)
        {
            _dbType = EDbConnectionTypes.MariaDb;
            _connectionString = connectionString;
        }

        public IDbConnection GetOpenConnection()
        {
            return DbConnectionFactory.GetDbConnection(_dbType, _connectionString);
        }
        public abstract IEnumerable<TEntity> GetAllById(long aSingleId);
        public abstract IEnumerable<TEntity> GetAll();
        public abstract TEntity GetSingle(long aSingleId);
        public abstract TEntity GetIntSingle(int aSingleId);
        public abstract void Insert(TEntity entity);
        public abstract long InsertToGetId(TEntity entity);
        public abstract void Update(TEntity entityToUpdate);

    }
}
