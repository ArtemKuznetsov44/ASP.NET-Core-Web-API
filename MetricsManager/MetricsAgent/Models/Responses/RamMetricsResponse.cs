using MetricsAgent.Models.DTO;

namespace MetricsAgent.Models.Responses
{
    public class RamMetricsResponse
    {
        public IList<RamMetricDto> Metrics { get; set; }
    }
}
