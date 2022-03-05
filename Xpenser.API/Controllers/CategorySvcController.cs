using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xpenser.Models;
using Xpenser.API.DaCore;

namespace Xpenser.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CategorySvcController : Controller
    {
        private readonly ICategoryRepository CatRepo;
        public CategorySvcController(ICategoryRepository aCatRepo)
        {
            CatRepo = aCatRepo;
        }

        [HttpGet("GetAllCategories")]
        public IActionResult GetAllCategories()
        {
            var vReturnVal = CatRepo.GetAll();
            return Ok(vReturnVal);
        }
        [Route("[action]/{aSingleId}")]
        [HttpGet]
        public IActionResult GetSingleCategory(long aSingleId)
        {
            var vReturnVal = CatRepo.GetSingle(aSingleId);
            return Ok(vReturnVal);
        }

        [HttpPost("CreateCategory")]
        public IActionResult CreateCategory([FromBody] Category aObject)
        {
            if (aObject == null)
            { return BadRequest(); }

            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }
            aObject.CategoryId = CatRepo.InsertToGetId(aObject);
            return Ok(aObject);
        }

        [HttpPut("UpdateCategory")]
        public IActionResult UpdateCategory([FromBody] Category aObject)
        {
            if (aObject == null)
            { return BadRequest(); }
            CatRepo.Update(aObject);
            return Ok(aObject);
        }
    }
}
