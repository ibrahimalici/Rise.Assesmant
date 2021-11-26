using AutoMapper;

namespace ContactsAPI.Mappings
{
    public class AutoMapperConfigurations : Profile
    {
        public AutoMapperConfigurations()
        {
            CreateMap<ContactsAPI.Entities.Contact, SharedLibrary.Domains.ContactsDTO>();
            CreateMap<ContactsAPI.Entities.ContactDetail, SharedLibrary.Domains.ContactDetailsDTO>();
            CreateMap<ContactsAPI.Entities.Report, SharedLibrary.Domains.ReportDTO>().ReverseMap();
        }
    }
}
