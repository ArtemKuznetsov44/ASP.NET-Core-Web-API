using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.DTO;
using MetricsAgent.Models.Requests;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    { 
        public MapperProfile()
        {
            #region Configuration for CpuMetrics:

            CreateMap<CpuMetric, CpuMetricDto>();
            CreateMap<CpuMetricCreateRequest, CpuMetric>()
                .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Value)) // For Value we do nothing.
                // For Time we should convert TimeSpan to seconds only and to int:
                .ForMember(x => x.Time, opt => opt.MapFrom(src => (int)src.Time.TotalSeconds));

            #endregion

            #region Configuration for DotNetMetrics:

            CreateMap<DotNetMetric, DotNetMetricDto>();
            CreateMap<DotNetMetricCreateRequest, DotNetMetric>()
                .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Value)) // For Value we do nothing.
                                                                               // For Time we should convert TimeSpan to seconds only and to int:
                .ForMember(x => x.Time, opt => opt.MapFrom(src => (int)src.Time.TotalSeconds));

            #endregion

            #region Configuration for HddMetrics:

            CreateMap<HddMetric, HddMetricDto>();
            CreateMap<HddMetricCreateRequest, HddMetric>()
                .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Value)) // For Value we do nothing.
                                                                               // For Time we should convert TimeSpan to seconds only and to int:
                .ForMember(x => x.Time, opt => opt.MapFrom(src => (int)src.Time.TotalSeconds));

            #endregion

            #region Configuration for NetworkMetrics:

            CreateMap<NetworkMetric, NetworkMetricDto>();
            CreateMap<NetworkMetricCreateRequest, NetworkMetric>()
                .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Value)) // For Value we do nothing.
                                                                               // For Time we should convert TimeSpan to seconds only and to int:
                .ForMember(x => x.Time, opt => opt.MapFrom(src => (int)src.Time.TotalSeconds));

            #endregion

            #region Configuration for RamMetrics:

            CreateMap<RamMetric, RamMetricDto>();
            CreateMap<RamMetricCreateRequest, RamMetric>()
                .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Value)) // For Value we do nothing.
                                                                               // For Time we should convert TimeSpan to seconds only and to int:
                .ForMember(x => x.Time, opt => opt.MapFrom(src => (int)src.Time.TotalSeconds));

            #endregion
        }
    }
}
