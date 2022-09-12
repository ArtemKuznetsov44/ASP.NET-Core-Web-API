using MetricsAgent.Services;
using MetricsAgent.Services.Implimintation;
using Microsoft.Extensions.DependencyInjection;
using Quartz; // Return a job by specified timeTable. 
using System.Diagnostics;


namespace MetricsAgent.Job
{
    public class RamMetricJob : IJob
    {
        // For dataBase usage.
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly PerformanceCounter _ramCounter;

        public RamMetricJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _ramCounter = new PerformanceCounter(
                categoryName: "Memory",
                counterName: "Available MBytes"
               );
        }

        public Task Execute(IJobExecutionContext context)
        {

            using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
            {
                var ramMetricsRepository = serviceScope.ServiceProvider.GetService<IRamMetricsRepository>();
                try
                {
                    float ramUsage = _ramCounter.NextValue(); // Get a ram metric value. 
                    var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()); // Get time in seconds.
                    Debug.WriteLine($"{time} > {ramUsage}");

                    ramMetricsRepository.Create(new Models.RamMetric
                    {
                        Value = (int)ramUsage,
                        Time = (long)time.TotalSeconds
                    });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"RamMetricJob : {ex.Message}");
                }
            }

            return Task.CompletedTask;
        }
    }
}

