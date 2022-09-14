using MetricsAgent.Models.DTO;

namespace MetricsAgent.Models.Responses
{
    public class HddMetricsResponse
    {
        public IList<HddMetricDto> Metrics { get; set; }
    }
}
