using Application.Models;
using Application.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalletServiceController : ControllerBase
    {
        private readonly IPalletService _palletService;

        public PalletServiceController(IPalletService palletService)
        {
            _palletService = palletService;
        }

        [Route("GetPallet")]
        [HttpPost]
        public Pallet GetPallet(string id)
        {
            var data = _palletService.Get(id);
            return data;
        }
    }
}
