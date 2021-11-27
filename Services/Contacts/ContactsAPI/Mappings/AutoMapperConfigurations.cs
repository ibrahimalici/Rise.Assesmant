using AutoMapper;

namespace ContactsAPI.Mappings
{
    public class AutoMapperConfigurations : Profile
    {
        public AutoMapperConfigurations()
        {
            CreateMap<ContactsAPI.Entities.Contact, SharedLibrary.Domains.ContactDTO>();
            CreateMap<ContactsAPI.Entities.ContactDetail, SharedLibrary.Domains.ContactDetailDTO>();
            CreateMap<ContactsAPI.Entities.Report, SharedLibrary.Domains.ReportDTO>().ReverseMap();
        }
    }
}
