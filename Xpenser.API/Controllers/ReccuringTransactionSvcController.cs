using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xpenser.API.Repositories;
using Xpenser.Models;


namespace Xpenser.API.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class ReccuringTransactionSvcController : Controller
    {
        private readonly IReccuringTransactionRepository reccuringTransRepo;
        public ReccuringTransactionSvcController(IReccuringTransactionRepository aReccuringTransRepo)
        {
            reccuringTransRepo = aReccuringTransRepo;
        }

        [HttpGet("GetAllReccuringTransactions")]
        public IActionResult GetAllReccuringTransactions()
        {
            var vReturnVal = reccuringTransRepo.GetAll();
            return Ok(vReturnVal);
        }

        [Route("[action]/{aSingleId}")]
        [HttpGet]
        public IActionResult GetSingleReccuringTransaction(long aSingleId)
        {
            var vReturnVal = reccuringTransRepo.GetSingle(aSingleId);
            return Ok(vReturnVal);
        }

        [HttpPost("CreateReccuringTransaction")]
        public IActionResult CreateReccuringTransaction([FromBody] ReccuringTransaction aObject)
        {
            if (aObject == null)
            { return BadRequest(); }

            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }
            aObject.ReccurTransId = reccuringTransRepo.InsertToGetId(aObject);
            return Ok(aObject);
        }

        [HttpPut("UpdateReccuringTransaction")]
        public IActionResult UpdateReccuringTransaction([FromBody] ReccuringTransaction aObject)
        {
            if (aObject == null)
            { return BadRequest(); }
            reccuringTransRepo.Update(aObject);
            return Ok(aObject);
        }
    }
}
