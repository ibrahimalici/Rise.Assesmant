using AutoMapper;

namespace ReportsAPI.Mappings
{
    public class AutoMapperConfigurations : Profile
    {
        public AutoMapperConfigurations()
        {
            CreateMap<ReportsAPI.Entities.Iletisim, SharedLibrary.Messages.IletisimDTO>();
            CreateMap<ReportsAPI.Entities.Kisi, SharedLibrary.Messages.KisiDTO>();
        }
    }
}
