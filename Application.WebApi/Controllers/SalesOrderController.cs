using Application.Models.ViewModels;
using Application.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrderController : ControllerBase
    {
        IDashboardNotificationService _dashboardService;

        public SalesOrderController(IDashboardNotificationService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [Route("EBStatusValidate")]
        [HttpPost]
        public async Task<IActionResult> EBStatusValidate(string id)
        {
            try
            {
                DashboardNotificationViewModel item = new DashboardNotificationViewModel();
                item.DocEntry = Convert.ToInt32(id);
                var data = await _dashboardService.UpdateEBStatusSO(item, "For Loading Validation");
                return Ok(data);

            }
            catch (Exception e)
            {
                //_logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }

		[Route("UpdateSalesOrder")]
		[HttpGet]
		public IActionResult UpdateSalesOrder(int salesOrderDocNum)
		{
			try
			{
				var data = _dashboardService.Get(salesOrderDocNum);
				return Ok(data);

			}
			catch (Exception e)
			{
				//_logger.LogError(e, e.Message);
				return BadRequest(e.Message);
			}
		}

		[Route("GetAllDashBoardNotif")]
		[HttpGet]
		public IActionResult GetAllDashBoardNotif()
		{
			try
			{
				var data = _dashboardService.GetAllSODashboardMobile().Result;
				return Ok(data);
			}
			catch (Exception e)
			{
				//_logger.LogError(e, e.Message);
				return BadRequest(e.Message);
			}
		}
	}
}
