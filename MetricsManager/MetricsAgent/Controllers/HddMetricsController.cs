using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MetricsAgent.Models.DTO;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        #region Services:

        private readonly ILogger<HddMetricsController> _logger;
        private readonly IHddMetricsRepository _hddMetricRepository;
        private readonly IMapper _mapper;

        #endregion

        public HddMetricsController(
            ILogger<HddMetricsController> logger,
            IHddMetricsRepository hddMetricRepository, 
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _hddMetricRepository = hddMetricRepository;
        }

        // Method for creation HddMetric:
        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            _hddMetricRepository.Create(_mapper.Map<HddMetric>(request));
            return Ok();
        }

        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<HddMetricDto>> GetLeftMetricsByPeriod([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsAgent/HddMetricsController/GetLeftMetricsByPeriod params:\n" +
                $"fromTime: {fromTime},\n" +
                $"toTime: {toTime}");
            return Ok(_hddMetricRepository.GetByTimePeriod(fromTime, toTime)
                .Select(metric => _mapper.Map<HddMetricDto>(metric)).ToList());
        }

        [HttpGet("all")]
        public ActionResult<IList<HddMetricDto>> GetAllHddMetrics() => Ok(_hddMetricRepository.GetAll()
            .Select(metric => _mapper.Map<HddMetricDto>(metric)).ToList());
    }
}
