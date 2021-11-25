using SharedLibrary.Domains;
using SharedLibrary.Messages;
using System;

namespace ReportsAPI.Entities
{
    public class Iletisim : BaseEntity
    {
        public Guid Id { get; set; }
        public ContactDetailType BilgiTipi { get; set; }
        public string BilgiIcerigi { get; set; }
        public Guid KisiId { get; set; }
        public Kisi Kisi { get; set; }
    }
}
