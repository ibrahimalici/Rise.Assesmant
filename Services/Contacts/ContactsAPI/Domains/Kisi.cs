using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.Domains
{
    public class KisiDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }

        public List<IletisimDTO> IletisimBilgileri = new List<IletisimDTO>();
    }
}
