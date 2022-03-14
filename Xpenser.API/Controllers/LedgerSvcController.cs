using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xpenser.API.DaCore;
using Xpenser.Models;

namespace Xpenser.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LedgerSvcController : Controller
    {
        private readonly ILedgerRepository LedgerRepo;
        public LedgerSvcController(ILedgerRepository aLedgerRepo)
        {
            LedgerRepo = aLedgerRepo;
        }

        [HttpGet("GetAllRecords")]
        public IActionResult GetAllRecords()
        {
            var vReturnVal = LedgerRepo.GetAll();
            return Ok(vReturnVal);
        }
        [Route("[action]/{aSingleId}")]
        [HttpGet]
        public IActionResult GetUserRecords(long aAppUserId)
        {
            var vReturnVal = LedgerRepo.GetAllById(aAppUserId);
            return Ok(vReturnVal);
        }
        [Route("[action]/{aSingleId}")]
        [HttpGet]
        public IActionResult GetSingleRecord(long aSingleId)
        {
            var vReturnVal = LedgerRepo.GetSingle(aSingleId);
            return Ok(vReturnVal);
        }

        [HttpPost("CreateRecord")]
        public IActionResult CreateRecord([FromBody] Ledger aObject)
        {
            if (aObject == null)
            { return BadRequest(); }

            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }
            aObject.TransId = LedgerRepo.InsertToGetId(aObject);
            return Ok(aObject);
        }

        [HttpPut("UpdateRecord")]
        public IActionResult UpdateRecord([FromBody] Ledger aObject)
        {
            if (aObject == null)
            { return BadRequest(); }
            LedgerRepo.Update(aObject);
            return Ok(aObject);
        }

    }
}
