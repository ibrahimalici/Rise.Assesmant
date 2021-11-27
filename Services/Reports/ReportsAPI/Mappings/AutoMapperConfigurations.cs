using AutoMapper;

namespace ReportsAPI.Mappings
{
    public class AutoMapperConfigurations : Profile
    {
        public AutoMapperConfigurations()
        {
            CreateMap<ReportsAPI.Entities.ContactDetails, SharedLibrary.Domains.ContactDetailsDTO>();
            CreateMap<ReportsAPI.Entities.Contacts, SharedLibrary.Domains.ContactsDTO>();
            CreateMap<ReportsAPI.Entities.Report, SharedLibrary.Domains.ReportDTO>().ReverseMap();
            CreateMap<ReportsAPI.Entities.ReportDetail, SharedLibrary.Domains.ReportDetailDTO>().ReverseMap();
        }
    }
}
