using Microsoft.AspNetCore.Mvc;
using ParkingAPI.DTO;
using ParkingAPI.Mappers;
using ParkingAPI.Services;
using ParkingAPI.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingAPI.Controllers
{
    //[ApiVersion("1")]
    [ApiController]
    [Route("api/v1/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _companyService.GetAllAsync();
            return Ok(result.Select(company => CompanyMap.ModelToDto(company)));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _companyService.GetAsync(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
           await _companyService.DeleteAsync(id);
           return Ok();
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] CompanyDto CompanyDto)
        {
            if(!ModelState.IsValid) return BadRequest();
            var response = await _companyService.CreateAsync(CompanyMap.DtoToModel(CompanyDto));
            return CreatedAtAction(nameof(Get), new { id = response.Id }, CompanyMap.ModelToDto(response));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CompanyDto CompanyDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var updatedCompany = (await _companyService.UpdateAsync(id, CompanyMap.DtoToModel(CompanyDto)));
            return Ok(CompanyMap.ModelToDto(updatedCompany));
        }
    }
}
