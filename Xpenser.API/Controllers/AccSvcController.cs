using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xpenser.Models;

namespace Xpenser.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccSvcController : Controller
    {
        private readonly IAccountRepository AccRepo;
        public AccSvcController(IAccountRepository aAccRepo)
        {
            AccRepo = aAccRepo;
        }

        [HttpGet("GetAllAccounts")]
        public IActionResult GetAllAccounts()
        {
            var vReturnVal = AccRepo.GetAll();
            return Ok(vReturnVal);
        }
        [Route("[action]/{aSingleId}")]
        [HttpGet]
        public IActionResult GetSingleAccount(long aSingleId)
        {
            var vReturnVal = AccRepo.GetSingle(aSingleId);
            return Ok(vReturnVal);
        }

        [HttpPost("CreateAccount")]
        public IActionResult CreateAccount([FromBody] Account aObject)
        {
            if (aObject == null)
            { return BadRequest(); }

            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }
            aObject.AccountId = AccRepo.InsertToGetId(aObject);
            return Ok(aObject);
        }

        [HttpPut("UpdateAccount")]
        public IActionResult UpdateAccount([FromBody] Account aObject)
        {
            if (aObject == null)
            { return BadRequest(); }
            AccRepo.Update(aObject);
            return Ok(aObject);
        }

    }
}
