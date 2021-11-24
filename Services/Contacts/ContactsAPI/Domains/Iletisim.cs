using System;

namespace ContactsAPI.Domains
{
    public class IletisimDTO
    {
        public Guid Id { get; set; }
        public ContactDetailType BilgiTipi { get; set; }
        public string BilgiIcerigi { get; set; }
        public Guid KisiId { get; set; }
        public KisiDTO Kisi { get; set; }
    }

}
