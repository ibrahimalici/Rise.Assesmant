using System;

namespace ReportsAPI.Entities
{
    public class ReportDetail:BaseEntity
    {
        public Guid ReportDetailId { get; set; }
        public Guid ReportId { get; set; }
        public string Location { get; set; }
        public int ContactCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
