using Application.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchSerialController : ControllerBase
    {
        private readonly IBatchSerialService _batchSerialService;
        private readonly ILogger<BatchSerialController> _logger;
        public BatchSerialController(IBatchSerialService batchSerialService, ILogger<BatchSerialController> logger)
        {
            _batchSerialService = batchSerialService;
            _logger = logger;
        }

        [Route("GetBatchSerialByMnfSerialLoc")]
        [HttpGet]
        public async Task<ActionResult> GetBatchSerialByMnfSerialLoc(string itemCode, string mnfSerial, string location)
        {
            try
            {
                var data = await _batchSerialService.GetBatchSerialByMnfSerialLoc(itemCode, mnfSerial, location);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }
        [Route("GetBinByBatchSerialLoc")]
        [HttpGet]
        public async Task<ActionResult> GetBinByBatchSerialLoc(string itemCode, string distNumber, string location)
        {
            try
            {
                var data = await _batchSerialService.GetBinByBatchSerialLoc(itemCode, distNumber, location);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }
        [Route("GetBinByLoc")]
        [HttpGet]
        public async Task<ActionResult> GetBinByLoc(string location)
        {
            try
            {
                var data = await _batchSerialService.GetBinByLoc(location);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
