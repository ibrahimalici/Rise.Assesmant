using AutoMapper;

namespace ContactsAPI.Mappings
{
    public class AutoMapperConfigurations : Profile
    {
        public AutoMapperConfigurations()
        {
            CreateMap<ContactsAPI.Entities.Iletisim, ContactsAPI.Domains.IletisimDTO>();
            CreateMap<ContactsAPI.Entities.Kisi, ContactsAPI.Domains.KisiDTO>();
        }
    }
}
