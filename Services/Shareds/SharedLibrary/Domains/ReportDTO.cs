using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Domains
{
    public class ReportDTO
    {
        public Guid ReportId { get; set; }
        public DateTime ReportDemandDateTime { get; set; }
        public ReportStatus ReportStatus { get; set; }
        public List<ReportDetailDTO> Reports { get; set; }
    }

    public class ReportDetailDTO
    {
        public Guid ReportId { get; set;}
        public string Location { get; set; }
        public int ContactCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
