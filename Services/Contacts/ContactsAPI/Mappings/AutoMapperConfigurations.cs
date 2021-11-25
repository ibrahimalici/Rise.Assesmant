using AutoMapper;

namespace ContactsAPI.Mappings
{
    public class AutoMapperConfigurations : Profile
    {
        public AutoMapperConfigurations()
        {
            CreateMap<ContactsAPI.Entities.Kisi, SharedLibrary.Messages.KisiDTO>();
            CreateMap<ContactsAPI.Entities.Iletisim, SharedLibrary.Messages.IletisimDTO>();
        }
    }
}
