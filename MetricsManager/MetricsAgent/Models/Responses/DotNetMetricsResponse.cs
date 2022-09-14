using MetricsAgent.Models.DTO;

namespace MetricsAgent.Models.Responses
{
    public class DotNetMetricsResponse
    {
        public IList<DotNetMetricDto> Metrics { get; set; }
    }
}
