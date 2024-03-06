using ParkingAPI.DTO;
using System;
using System.Threading.Tasks;

namespace ParkingAPI.Services.Interfaces
{
    public interface IReportService
    {
        Task<DetaildReportDTO> GetDetaildReportAsync(Guid companyId, DateTime StartTime, DateTime EndTime);
        Task<ReportDTO> GetdReportAsync(Guid companyId);
    }
}
