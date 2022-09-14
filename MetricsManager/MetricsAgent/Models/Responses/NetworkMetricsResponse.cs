using MetricsAgent.Models.DTO;

namespace MetricsAgent.Models.Responses
{
    public class NetworkMetricsResponse
    {
        public IList<NetworkMetricDto> Metrics { get; set; }
    }
}
