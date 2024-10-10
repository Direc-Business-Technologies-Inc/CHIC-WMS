using Application.Libraries.SAP.SL;
using Application.Libraries.Utilies;
using Application.Models.ViewModels;
using Application.Services.Core;
using Application.Services.Repositories;
using AutoMapper;
using DataManager.Models.InventoryTransfer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using static Application.Models.ViewModels.InventoryTransferViewModel;
using Application.Models;
using System.Runtime.InteropServices;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryTransferController : ControllerBase
    {
        private readonly IInventoryTransferService _invtyService;
        private readonly IWarehouseService _warehouseService;
        private readonly ILogger<InventoryTransferController> _logger;
        private readonly IMapper _mapper;

        public InventoryTransferController(IInventoryTransferService invtyService, IWarehouseService warehouseService, ILogger<InventoryTransferController> logger, IMapper mapper)
        {
            _invtyService = invtyService;
            _warehouseService = warehouseService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            try
            {
                var data = _invtyService.GetAsync(id);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }

        [Route("GetTransferType")]
        [HttpGet]
        public async Task<ActionResult> GetTransferType()
        {
            try
            {
                var data = await _invtyService.GetTransferType();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }

        [Route("GetServiceData")]
        [HttpGet]
        public ActionResult GetServiceData(string serviceType)
        {
            try
            {
                var data = _invtyService.GetServiceData(serviceType);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }

        ////POST api/<InventoryTransfer>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InventoryTransferModel value)
        {
            try
            {
                //var payload = _mapper.Map<InventoryTransferModel>(value);
                var data = await _invtyService.CreateAsync(value);
                return Ok(data);

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }

        [Route("GetPalletBatches")]
        [HttpGet]
        public ActionResult GetPalletBatches(string palletCode)
        {
            try
            {
                var data = _invtyService.GetPalletBatches(palletCode);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }
        [Route("GetPalletMatrix")]
        [HttpGet]
        public ActionResult GetPalletMatrix(string itemCode, string palletCode, string serviceType, [Optional] bool isManualTransfer, string binCode = "")
        {
            try
            {
                var data = _invtyService.GetPalletMatrix(itemCode, palletCode, binCode, serviceType, isManualTransfer);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                //throw;
                return BadRequest(e.Message);
            }
        }

        [Route("GetTransferTypeFromMatrix")]
        [HttpGet]
        public async Task<ActionResult> GetTransferTypeFromMatrix(string transferType, string serviceType)
        {
            try
            {
                var data = await _invtyService.GetTransferTypeFromMatrix(transferType, serviceType);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }

        [Route("BinOccupied")]
        [HttpGet]
        public async Task<ActionResult> BinOccupied(string binCode, string palletCode)
        {
            try
            {
                var data = _invtyService.BinOccupied(binCode, palletCode);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }

        }

        [Route("BinExists")]
        [HttpGet]
        public async Task<ActionResult> BinExists(string binCode)
        {
            try
            {
                var data = _invtyService.BinExists(binCode);
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
