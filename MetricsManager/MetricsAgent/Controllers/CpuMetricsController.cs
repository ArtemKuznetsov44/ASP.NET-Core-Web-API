using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        #region Services
        // Logger type should be CpuMetricsController because its work in it.
        private readonly ILogger<CpuMetricsController> _logger; // Adding logger field.
        private readonly ICpuMetricsRepository _cpuMetricsRepository;

        #endregion 
        
        public CpuMetricsController(
            ILogger<CpuMetricsController> logger, 
            ICpuMetricsRepository cpuMetricsRepository)
        {
            _logger = logger; // Intialize our logger in constructor:
            _cpuMetricsRepository = cpuMetricsRepository;
        }
        
        // Method for creating CpuMetric:
        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _cpuMetricsRepository.Create(new Models.CpuMetric
            {
                Value = request.Value,
                Time = (int)request.Time.TotalSeconds
            });
            return Ok();
        }

        // Method for getting CpuMetric-s objects by period:
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<CpuMetric>> GetCpuMetricsByPeriod([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsAgent/CpuMetricsController/GetMetricsByPeriod params:\n" +
                $"fromTime: {fromTime},\n" +
                $"toTime: {toTime}");
            return Ok(_cpuMetricsRepository.GetByTimePeriod(fromTime, toTime)); 
        }

        // Method returns a list with all CpuMetric-s objects which were created before:
        [HttpGet("all")]
        public ActionResult<IList<CpuMetric>> GetAllCpuMetrics() => Ok(_cpuMetricsRepository.GetAll()); 
    }
}
