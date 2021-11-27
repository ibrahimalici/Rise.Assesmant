using SharedLibrary.Domains;
using SharedLibrary.Messages;
using System;

namespace ReportsAPI.Entities
{
    public class ContactDetail : BaseEntity
    {
        public Guid ContactDetailId { get; set; }
        public ContactDetailType ContactDetailType { get; set; }
        public string Description { get; set; }
        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
