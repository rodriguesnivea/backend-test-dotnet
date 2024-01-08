using Microsoft.AspNetCore.Mvc;
using ParkingAPI.Exceptions;
using ParkingAPI.Repositories.Interfaces;
using ParkingAPI.Services.Interfaces;
using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace ParkingAPI.Controllers
{
    [ApiController]
    [Route("api/v1/companies")]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;

        public ParkingController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpPost("{companyId}/vehicles/{vehicleId}/check-in")]
        public async Task<IActionResult> CheckIn(Guid companyId, Guid vehicleId)
        {
            await _parkingService.CheckinAsync(companyId, vehicleId);
            return Ok();
        }

        [HttpPost("{companyId}/vehicles/{vehicleId}/check-out")]
        public async Task<IActionResult> CheckOut(Guid companyId, Guid vehicleId)
        {
            var result = await _parkingService.CheckoutAsync(companyId, vehicleId);
            return Ok();
        }
    }

}
