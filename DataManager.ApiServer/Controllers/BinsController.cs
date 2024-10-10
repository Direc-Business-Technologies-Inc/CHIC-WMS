using AutoMapper;
using DataManager.Models.APIs;
using DataManager.Models.Bins;
using DataManager.Services.Core;
using DataManager.Services.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataManager.ApiServer.Controllers
{
	[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
	[ApiController]
	public class BinsController : ControllerBase
	{
		private IMapper Mapper { get; set; }
		private IBinDataServices _binDataServices { get; set; }

        public BinsController(IMapper mapper, IBinDataServices binDataServices)
        {
            Mapper = mapper;
			_binDataServices = binDataServices;
        }

        // GET: api/<BinsController>
        [HttpGet]
		public IActionResult Get()
		{
			try
			{
				List<BinMapping> model = _binDataServices.GetBinMapping();
				List<BinMappingAPIModel> values = Mapper.Map<List<BinMappingAPIModel>>(model);
				return Ok(values);
			}
			catch (Exception ex)
			{
				return BadRequest($"{ex.Message}. Please contact your administrator.");
			}
		}

		// GET api/<BinsController>/5
		[HttpGet("{warehouseCode,shelf}")]
		public IActionResult Get(string warehouseCode, string shelf)
		{
			try
			{
				BinMapping model = _binDataServices.GetBinMapping(warehouseCode, shelf);
				BinMappingAPIModel value = Mapper.Map<BinMappingAPIModel>(model);
				return Ok(value);
			}
			catch (Exception ex)
			{
				return BadRequest($"{ex.Message}. Please contact your administrator.");
			}
		}

		// POST api/<BinsController>
		[HttpPost]
		public IActionResult Post([FromBody] BinMappingAPIModel value)
		{
			BinMapping model = Mapper.Map<BinMapping>(value);
			try
			{
				return Ok("modtakels");
			}
			catch (Exception ex)
			{
				return BadRequest($"{ex.Message}. Please contact your administrator.");
			}
		}

		// PUT api/<BinsController>/5
		[HttpPatch("{warehouseCode,shelf}")]
		public void Patch(string warehouseCode, string shelf, [FromBody] BinMappingAPIModel value)
		{

		}

		// DELETE api/<BinsController>/5
		//[HttpDelete("{id}")]
		//public void Delete(int id)
		//{
		//}
	}
}
