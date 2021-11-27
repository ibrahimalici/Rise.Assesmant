using SharedLibrary.Domains;
using System;
using System.Collections.Generic;

namespace ReportsAPI.Entities
{
    public class Report : BaseEntity
    {
        public Guid ReportId { get; set; }
        public DateTime ReportDemandDate { get; set; }
        public ReportStatus ReportStatus { get; set; }
        public List<ReportDetailDTO> Reports { get; }
    }
}
