using MetricsAgent.Services;
using MetricsAgent.Services.Implimintation;
using Microsoft.Extensions.DependencyInjection;
using Quartz; // Return a job by specified timeTable. 
using System.Diagnostics;

namespace MetricsAgent.Job
{
    public class DotNetMetricJob : IJob
    {

        // For dataBase usage.
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly PerformanceCounter _dotNetCounter;

        public DotNetMetricJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _dotNetCounter = new PerformanceCounter(
                categoryName: ".NET CLR Memory",
                counterName: "# Bytes in all Heaps", 
                instanceName: "_Global_");
        }

        public Task Execute(IJobExecutionContext context)
        {

            using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
            {
                var dotNetMetricsRepository = serviceScope.ServiceProvider.GetService<IDotNetMetricsRepository>();
                try
                {
                    float dotNetUsage = _dotNetCounter.NextValue(); // Get a dotNet metric value.
                    var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()); // Get time in seconds.
                    Debug.WriteLine($"{time} > {dotNetUsage}");

                    dotNetMetricsRepository.Create(new Models.MetricClasses.DotNetMetric
                    {
                        Value = (int)dotNetUsage,
                        Time = (long)time.TotalSeconds
                    });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"DotNetMetricJob : {ex.Message}");
                }
            }

            return Task.CompletedTask;
        }
    }
}

