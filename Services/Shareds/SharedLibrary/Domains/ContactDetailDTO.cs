using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Domains
{
    public class ContactDetailDTO
    {
        public Guid ContactDetailId { get; set; }
        public ContactDetailType ContactDetailType { get; set; }
        public string Decription { get; set; }
        public Guid ContactId { get; set; }
        public ContactDTO Kisi { get; set; }
    }
}
