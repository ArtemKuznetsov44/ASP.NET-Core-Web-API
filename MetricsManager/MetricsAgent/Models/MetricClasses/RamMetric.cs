namespace MetricsAgent.Models.MetricClasses
{
    public class RamMetric
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public long Time { get; set; } // TimeSpan format will be look like seconds only for DataBase
    }
}
