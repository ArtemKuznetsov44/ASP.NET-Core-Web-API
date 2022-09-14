using AutoMapper;
using MetricsManager.Models;
using MetricsManager.Models.DTO;

namespace MetricsManager
{
    public class MapperProfile : Profile 
    {
        public MapperProfile()
        {
            CreateMap<AgentInfo, AgentInfoDto>()
                .ForMember(x => x.AgentId, opt => opt.MapFrom(src => src.AgentId))
                .ForMember(x => x.AgentUri, opt => opt.MapFrom(src => @src.AgentUri.ToString()))
                .ForMember(x => x.Enable, opt => opt.MapFrom(src => src.Enable)); 
        }
    }
}
