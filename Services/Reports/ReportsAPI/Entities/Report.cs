using SharedLibrary.Domains;
using System;
using System.Collections.Generic;

namespace ReportsAPI.Entities
{
    public class Report : BaseEntity
    {
        public Guid ReportId { get; set; }
        public DateTime ReportDemandDateTime { get; set; }
        public ReportStatus ReportStatus { get; set; }
        public List<ReportDetail> Reports { get; set; }
    }
}
