using ParkingAPI.DTO;
using System;
using System.Threading.Tasks;

namespace ParkingAPI.Services.Interfaces
{
    public interface IReportService
    {
        Task<DetaildReportDto> GetDetaildReportAsync(Guid companyId, DateTime StartTime, DateTime EndTime);
        Task<ReportDto> GetdReportAsync(Guid companyId);
    }
}
