using Application.Models;
using Application.Services.Core;
using Application.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalletsController : ControllerBase
    {

        private readonly IPalletService _palletService;
        private readonly ILogger<PalletsController> _logger;

        public PalletsController(IPalletService palletService, ILogger<PalletsController> logger)
        {
            _palletService = palletService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            try
            {
                var data = _palletService.Get(id);
                //return data;
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }
        [Route("GetIsExistBincode")]
        [HttpGet]
        public async Task<ActionResult> GetIsExistBincode(string palletCode, string binCode)
        {
            try
            {
                var data = await _palletService.GetIsExistBincode(palletCode, binCode);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }

        [Route("IsForRelease")]
        [HttpGet]
        public async Task<ActionResult> IsForRelease(string palletCode)
        {

            try
            {
                var data = _palletService.IsForRelease(palletCode);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }

		[Route("ExtractPalletDetails")]
		[HttpGet]
		public async Task<ActionResult> ExtractPalletDetails(string data)
		{
			try
			{
				var extractedData = _palletService.ExtractPalletDetails(data);
				return Ok(extractedData);
			}
			catch (Exception e)
			{
				_logger.LogError(e, e.Message);
				return BadRequest(e.Message);
			}
		}

		//[HttpGet("{id}")]
		//public Pallet Get(string id)
		//{
		//    try
		//    {
		//        var data = _palletService.Get(id);
		//        //return data;
		//        return Ok(data);
		//    }
		//    catch (Exception e)
		//    {
		//        _logger.LogError(e, e.Message);
		//        return BadRequest(e.Message);
		//    }
		//}
	}
}
