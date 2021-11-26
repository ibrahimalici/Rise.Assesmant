using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Domains
{
    public class ContactDetailsDTO
    {
        public Guid Id { get; set; }
        public ContactDetailType BilgiTipi { get; set; }
        public string BilgiIcerigi { get; set; }
        public Guid KisiId { get; set; }
        public ContactsDTO Kisi { get; set; }
    }
}
