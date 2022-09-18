using MetricsManager.Models.MetricClasses;
using System.Text.Json.Serialization;

namespace MetricsManager.Models.Responses
{
    public class DotNetMetricsResponse
    {
        // Agent identifier whitch we should not return in response^
        public int AgentId { get; set; }

        // A collection of cpu metrics:
        [JsonPropertyName("metrics")]
        public DotNetMetric[]? Metrics { get; set; }
    }
}
