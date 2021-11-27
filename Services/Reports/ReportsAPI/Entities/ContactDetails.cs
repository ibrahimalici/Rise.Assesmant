using SharedLibrary.Domains;
using SharedLibrary.Messages;
using System;

namespace ReportsAPI.Entities
{
    public class ContactDetails : BaseEntity
    {
        public Guid ContactDetailId { get; set; }
        public ContactDetailType BilgiTipi { get; set; }
        public string BilgiIcerigi { get; set; }
        public Guid KisiId { get; set; }
        public Contacts Kisi { get; set; }
    }
}
