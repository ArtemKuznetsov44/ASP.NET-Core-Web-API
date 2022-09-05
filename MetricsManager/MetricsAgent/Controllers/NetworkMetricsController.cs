using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        #region Services

        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _networkMetricsRepository;

        #endregion 

        public NetworkMetricsController(
            ILogger<NetworkMetricsController> logger,
            INetworkMetricsRepository networkMetricsRepository)
        {
            _logger = logger;
            _networkMetricsRepository = networkMetricsRepository;
        }

        // Method for creation NetworkMetric:
        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            _networkMetricsRepository.Create(new Models.NetworkMetric
            {
                Value = request.Value,
                Time = (int)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<NetworkMetric>> GetMetricsByPeriod([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsAgent/NetworkMetricsController/GetMetricsByPeriod params:\n" +
               $"fromTime: {fromTime},\n" +
               $"toTime: {toTime}");
            return Ok(_networkMetricsRepository.GetByTimePeriod(fromTime, toTime));
        }
    }
}
