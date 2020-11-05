using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xpenser.API.Repositories;
using Xpenser.Models;

namespace Xpenser.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppUserSvcController : ControllerBase
    {
        private readonly IAppUserRepository UserRepo;

        public AppUserSvcController(IAppUserRepository aUserRepo)
        {
            UserRepo = aUserRepo;
        }
        [HttpGet]
        public List<AppUser> GetAll()
        {
            var users = new List<AppUser>
            {
                new AppUser {FirstName="Ajay" },
                new AppUser {FirstName="Saurabh" },
            };
            return users;
        }


    }
}
