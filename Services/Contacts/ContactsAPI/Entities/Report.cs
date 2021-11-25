using SharedLibrary.Domains;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.Entities
{
    public class Report
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime RaporTalepTarihi { get; set; }
        public ReportStatus RaporDurumu { get; set; }
    }
}
