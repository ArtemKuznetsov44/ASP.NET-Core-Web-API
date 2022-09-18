using MetricsAgent.Services;
using MetricsAgent.Services.Implimintation;
using Microsoft.Extensions.DependencyInjection;
using Quartz; // Return a job by specified timeTable. 
using System.Diagnostics;

namespace MetricsAgent.Job
{
    public class CpuMetricJob : IJob
    {
        // For dataBase usage.
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly PerformanceCounter _cpuCounter; 

        public CpuMetricJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _cpuCounter = new PerformanceCounter(
                categoryName: "Processor",
                counterName: "% Processor Time",
                instanceName: "_Total");
        }
        
        public Task Execute(IJobExecutionContext context)
        {
            
            using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
            {
                var cpuMetricsRepository = serviceScope.ServiceProvider.GetService<ICpuMetricsRepository>(); 
                try
                {
                    float cpuUsageInPercents = _cpuCounter.NextValue(); // Get a cpu metric in %; 
                    var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()); // Get time in seconds.
                    Debug.WriteLine($"{time} > {cpuUsageInPercents}");

                    cpuMetricsRepository.Create(new Models.MetricClasses.CpuMetric
                    {
                        Value = (int)cpuUsageInPercents,
                        Time = (long)time.TotalSeconds
                    });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"CpuMetricJob : {ex.Message}");
                }
            }
           
            return Task.CompletedTask;
        }
    }
}
