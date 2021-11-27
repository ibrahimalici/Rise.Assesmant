using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Domains
{
    public class ContactDTO
    {
        public Guid ContactId { get; set; }
        public string Name { get; set; }
        public string Surename { get; set; }
        public string Company { get; set; }

        public List<ContactDetailDTO> ContactSubDetails = new List<ContactDetailDTO>();
    }
}
