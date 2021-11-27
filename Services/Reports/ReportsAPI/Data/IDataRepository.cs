using SharedLibrary.Domains;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportsAPI.Data
{
    public interface IDataRepository
    {
        List<ReportDTO> GetAllReportObjects();

        Task<ReportDTO> GetReportObject(Guid Id);

        Task<bool> PrepareReport(ReportDTO report);
    }
}
