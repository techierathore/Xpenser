﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly IAccountRepository AccRepo;
        public LedgerSvcController(ILedgerRepository aLedgerRepo,
            IAccountRepository aAccRepo)
        {
            LedgerRepo = aLedgerRepo;
            AccRepo = aAccRepo;
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
            UpdateAmount(aObject);
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

        private void UpdateAmount (Ledger aLedgerObject)
        {
            switch (aLedgerObject.TransType)
            {
                case AppConstants.IncomeType: 
                        AccRepo.AddAmount(aLedgerObject.SrcAccId, aLedgerObject.Amount);
                    break;
                case AppConstants.ExpenseType:
                    AccRepo.DeductAmount(aLedgerObject.SrcAccId, aLedgerObject.Amount);
                    break;
            }
        }

    }
}
