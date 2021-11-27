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
        public DateTime RaporTalepTarihi { get; set; }
        public ReportStatus RaporDurumu { get; set; }
        public List<ReportDetailDTO> Reports { get; set; }
    }

    public class ReportDetailDTO
    {
        public Guid ReportDetailId { get; set; }
        public Guid ReportId { get; set; }
        public string KonumBilgisi { get; set; }
        public int KisiSayisi { get; set; }
        public int TelSayisi { get; set; }
    }
}
