using AutoMapper;

namespace ReportsAPI.Mappings
{
    public class AutoMapperConfigurations : Profile
    {
        public AutoMapperConfigurations()
        {
            CreateMap<ReportsAPI.Entities.Iletisim, SharedLibrary.Domains.IletisimDTO>();
            CreateMap<ReportsAPI.Entities.Kisi, SharedLibrary.Domains.KisiDTO>();
            CreateMap<ReportsAPI.Entities.Report, SharedLibrary.Domains.ReportDTO>().ReverseMap();
            CreateMap<ReportsAPI.Entities.ReportDetail, SharedLibrary.Domains.ReportDetailDTO>().ReverseMap();
        }
    }
}
