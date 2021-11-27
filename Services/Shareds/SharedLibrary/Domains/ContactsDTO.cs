using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Domains
{
    public class ContactsDTO
    {
        public Guid ContactId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }

        public List<ContactDetailsDTO> IletisimBilgileri = new List<ContactDetailsDTO>();
    }
}
