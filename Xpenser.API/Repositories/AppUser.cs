using System;
using System.Collections.Generic;
using Xpenser.API.DataAccess;
using Xpenser.Models;

namespace Xpenser.API.Repositories
{
    public class AppUserRepo : MariaDbRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepo(string connectionString) : base(connectionString) { }
        public override IEnumerable<AppUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public override AppUser GetIntSingle(int aSingleId)
        {
            throw new NotImplementedException();
        }

        public override AppUser GetSingle(long aSingleId)
        {
            throw new NotImplementedException();
        }

        public override void Insert(AppUser entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(AppUser entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
