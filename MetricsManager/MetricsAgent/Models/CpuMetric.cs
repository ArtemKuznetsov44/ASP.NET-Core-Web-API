namespace MetricsAgent.Models
{
    public class CpuMetric
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int Time { get; set; } // TimeSpan format will be look like seconds only for DataBase
    }
}
