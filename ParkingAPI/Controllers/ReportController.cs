using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingAPI.Services.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ParkingAPI.Controllers
{
    [Authorize]
    [Route("api/v1/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("{companyId}")]
        public async Task<ActionResult> GetReport(Guid companyId)
        {
            var report = await _reportService.GetdReportAsync(companyId);
            return Ok(report);
        }

        [HttpPost("{companyId}/detailed")]
        public async Task<ActionResult> GetDetaildReport (Guid companyId, [DataType(DataType.Date)] DateTime startTime, [DataType(DataType.Date)] DateTime endTime)
        {
            var report = await _reportService.GetDetaildReportAsync(companyId, startTime, endTime);
            return Ok(report);
        }
    }
}
