using AutoMapper;

namespace ContactsAPI.Mappings
{
    public class AutoMapperConfigurations : Profile
    {
        public AutoMapperConfigurations()
        {
            CreateMap<ContactsAPI.Entities.Kisi, SharedLibrary.Domains.KisiDTO>();
            CreateMap<ContactsAPI.Entities.Iletisim, SharedLibrary.Domains.IletisimDTO>();
            CreateMap<ContactsAPI.Entities.Report, SharedLibrary.Domains.ReportDTO>().ReverseMap();
        }
    }
}
