using SharedLibrary.Domains;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportsAPI.Data
{
    public interface IDataRepository
    {
        Task<ReportDTO> GetReportObject();

        Task<bool> PrepareReport(ReportDTO report);
    }
}
