using Microsoft.AspNetCore.Mvc;
using ParkingAPI.DTO;
using ParkingAPI.Mappers;
using ParkingAPI.Services.Interfaces;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace ParkingAPI.Controllers
{
    [Route("api/v1/vehicles")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _vehicleService.GetAllAsync();
            return Ok(result.Select(vehicle => VehicleMap.ModelToDto(vehicle)));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _vehicleService.GetAsync(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _vehicleService.DeleteAsync(id);
            return Ok();
        }
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] VehicleDto vehicle)
        {
            if (!ModelState.IsValid) return BadRequest();
            var response = await _vehicleService.CreateAsync(VehicleMap.DtoToModel(vehicle));
            return CreatedAtAction(nameof(Get), new { id = response.Id }, VehicleMap.ModelToDto(response));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] VehicleDto vehicle)
        {
            if (!ModelState.IsValid) return BadRequest();
            var updatedVehicle = (await _vehicleService.UpdateAsync(id,VehicleMap.DtoToModel(vehicle)));
            return Ok(VehicleMap.ModelToDto(updatedVehicle));
        }
    }
}
