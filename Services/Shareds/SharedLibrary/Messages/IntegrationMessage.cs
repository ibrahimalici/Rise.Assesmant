using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Messages
{
    public class IntegrationMessage
    {
        public KisiDTO Kisi { get; set; }
    }

    public class IletisimDTO
    {
        public Guid Id { get; set; }
        public ContactDetailType BilgiTipi { get; set; }
        public string BilgiIcerigi { get; set; }
        public Guid KisiId { get; set; }
        public KisiDTO Kisi { get; set; }
    }

    public class KisiDTO
    {
        public Guid Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }

        public List<IletisimDTO> IletisimBilgileri = new List<IletisimDTO>();
    }

    public enum ContactDetailType
    {
        TelNo = 1,
        Email = 2,
        Konum = 3
    }
}
