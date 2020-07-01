using Microsoft.AspNetCore.Mvc;
using Xpenser.API.Repositories;

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



    }
}
