using Application.Services.Repositories;
using DataManager.Models.Receiving;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.Models.ViewModels.ReceivingViewModel;

namespace Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceivingController : ControllerBase
    {
        private readonly IReceivingService _receivingService;
        private readonly ILogger<ReceivingController> _logger;

        public ReceivingController(IReceivingService receivingService, ILogger<ReceivingController> logger)
        {
            _receivingService = receivingService;
            _logger = logger;
        }

        // POST api/<ReceivingController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReceivingModel value)
        {
            try
            {
                var data = await _receivingService.CreateAsync(value);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }

        [Route("SalesOrders")]
        [HttpGet]
        public List<SalesOrder> SalesOrders()
        {
            var data = _receivingService.GetSalesOrderList();
            return data;
        }
    }
}
