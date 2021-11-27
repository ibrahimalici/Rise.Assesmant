using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportsAPI.Entities
{
    public class Contacts : BaseEntity
    {
        public Guid ContactId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }

        public List<ContactDetails> IletisimBilgileri = new List<ContactDetails>();
    }
}
