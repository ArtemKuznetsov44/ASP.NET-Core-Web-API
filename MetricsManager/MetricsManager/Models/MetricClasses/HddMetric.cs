using System.Text.Json.Serialization;

namespace MetricsManager.Models.MetricClasses
{
    public class HddMetric
    {
        [JsonPropertyName("value")]
        public int Value { get; set; }

        [JsonPropertyName("time")]
        public int Time { get; set; }
    }
}
