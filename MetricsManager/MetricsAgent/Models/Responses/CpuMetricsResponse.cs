using MetricsAgent.Models.DTO;

namespace MetricsAgent.Models.Responses
{
    public class CpuMetricsResponse
    {
        public IList<CpuMetricDto> Metrics { get; set; }
    }
}
