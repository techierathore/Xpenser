using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xpenser.Models;


namespace Xpenser.API.Controllers
{
    //[Authorize] 
    [ApiController]
    [Route("[controller]")]
    public class TargetSvcController : Controller
    {
        private readonly ITargetRepository TarRepo;
        public TargetSvcController(ITargetRepository aTarRepo)
        {
            TarRepo = aTarRepo;
        }

        [HttpGet("GetTarget")]
        public IActionResult GetAllTarget()
        {
            var vReturnVal = TarRepo.GetAll();
            return Ok(vReturnVal);
        }

        [Route("GetTarget/{aSingleId}")]
        [HttpGet]
        public IActionResult GetSingleTarget(long aSingleId)
        {
            var vReturnVal = TarRepo.GetSingle(aSingleId);
            return Ok(vReturnVal);
        }

        [HttpPost("CreateTarget")]
        public IActionResult CreateTarget([FromBody] Target aObject)
        {
            if (aObject == null)
            { return BadRequest(); }

            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }
            TarRepo.Insert(aObject);
            return Ok();
        }

        [HttpPut("UpdateTarget")]
        public IActionResult UpdateTarget([FromBody] Target aObject)
        {
            if (aObject == null)
            { return BadRequest(); }
            TarRepo.Update(aObject);
            return Ok(aObject);
        }
    }
}
