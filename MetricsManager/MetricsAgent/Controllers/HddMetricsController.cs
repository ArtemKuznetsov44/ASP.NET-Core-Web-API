using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        #region Services

        private readonly ILogger<HddMetricsController> _logger;
        private readonly IHddMetricsRepository _hddMetricRepository;

        #endregion 

        public HddMetricsController(
            ILogger<HddMetricsController> logger,
            IHddMetricsRepository hddMetricRepository)
        {
            _logger = logger;
            _hddMetricRepository = hddMetricRepository;
        }

        // Method for creation HddMetric:
        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            _hddMetricRepository.Create(new Models.HddMetric
            {
                Value = request.Value,
                Time = (int)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<HddMetric>> GetLeftMetricsByPeriod([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsAgent/HddMetricsController/GetLeftMetricsByPeriod params:\n" +
                $"fromTime: {fromTime},\n" +
                $"toTime: {toTime}");
            return Ok(_hddMetricRepository.GetByTimePeriod(fromTime, toTime));
        }
    }
}
