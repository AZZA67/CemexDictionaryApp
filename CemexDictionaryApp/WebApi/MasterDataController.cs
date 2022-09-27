using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CemexDictionaryApp.Repositories;
using CemexDictionaryApp.WebApi.ApiModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CemexDictionaryApp.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        readonly MasterDataRepository MasterRepository;
        public MasterDataController(MasterDataRepository masterRepository)
        {
            MasterRepository = masterRepository;
        }

        [HttpGet("MasterData")]
        public IActionResult StateList()
        {
            var _result = MasterRepository.StateList();
            var _occupationList = MasterRepository.OccupationList();
            if (_result != null)
                return Ok(new { Flag = true, Message = "Done", States = StateMapping.Mapping(_result), Occupation = _occupationList});
            else
                return BadRequest(new { Flag = false, Message = "Error _ Empty List", Data = 0 });
        }
    }
}
