using Application.Models.DTOs;
using Application.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispatchController : ControllerBase
    {
        private readonly IDispatchService _dispatchService;
        private readonly IMapper _mapper;

        public DispatchController(IDispatchService dispatchService, IMapper mapper)
        {
            _dispatchService = dispatchService;
            _mapper = mapper;
        }

        // GET: api/<Dispatch>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            throw new NotImplementedException();
            return new string[] { "value1", "value2" };
        }

        // GET api/<Dispatch>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            throw new NotImplementedException();
            return "value";
        }

        [HttpGet]
        [Route("GetDispatchableSalesOrder")]
        public async Task<ActionResult> GetDispatchableSalesOrder()
        {
            try
            {
                var res = _dispatchService.GetDispatchableSalesOrders();
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route("AllBoxIsReleased")] // TODO: rename AllBoxIsReleased to AllBoxIsDispatched. Also rename the http clients using this endpoints
        public async Task<ActionResult> AllBoxIsReleased(string[] palletCodes)
        {
            try
            {
                var res = _dispatchService.AllBoxIsDispatched(palletCodes);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // POST api/<Dispatch>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateDispatchDTO model)
        {
            try
            {
                var data = _mapper.Map<DispatchModel>(model);
                var res = await _dispatchService.CreateAsync(data);
                return Ok();
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
