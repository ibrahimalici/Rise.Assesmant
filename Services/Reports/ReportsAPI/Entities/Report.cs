using SharedLibrary.Domains;
using System;
using System.Collections.Generic;

namespace ReportsAPI.Entities
{
    public class Report : BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime RaporTalepTarihi { get; set; }
        public ReportStatus RaporDurumu { get; set; }
        public List<ReportDetailDTO> Reports { get; }
    }
}
