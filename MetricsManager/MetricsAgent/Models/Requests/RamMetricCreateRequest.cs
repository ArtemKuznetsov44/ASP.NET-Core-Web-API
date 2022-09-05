namespace MetricsAgent.Models.Requests
{
    public class RamMetricCreateRequest
    {
        public int Value { get; set; } // Metric value.
        public TimeSpan Time { get; set; } // TimeSpan when metric was fixed.
    }
}
