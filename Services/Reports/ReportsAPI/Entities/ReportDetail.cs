using System;

namespace ReportsAPI.Entities
{
    public class ReportDetail:BaseEntity
    {
        public Guid ReportDetailId { get; set; }
        public Guid ReportId { get; set; }
        public string KonumBilgisi { get; set; }
        public int KisiSayisi { get; set; }
        public int TelSayisi { get; set; }
    }
}
