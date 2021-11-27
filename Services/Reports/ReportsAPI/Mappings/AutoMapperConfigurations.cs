using AutoMapper;

namespace ReportsAPI.Mappings
{
    public class AutoMapperConfigurations : Profile
    {
        public AutoMapperConfigurations()
        {
            CreateMap<ReportsAPI.Entities.ContactDetail, SharedLibrary.Domains.ContactDetailDTO>();
            CreateMap<ReportsAPI.Entities.Contact, SharedLibrary.Domains.ContactDTO>();
            CreateMap<ReportsAPI.Entities.Report, SharedLibrary.Domains.ReportDTO>().ReverseMap();
            CreateMap<ReportsAPI.Entities.ReportDetail, SharedLibrary.Domains.ReportDetailDTO>().ReverseMap();
        }
    }
}
