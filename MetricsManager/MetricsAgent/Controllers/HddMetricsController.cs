using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MetricsAgent.Models.DTO;
using MetricsAgent.Models.Responses;
using MetricsAgent.Models.MetricClasses;

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
        public ActionResult<HddMetricsResponse> GetLeftMetricsByPeriod(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsAgent/HddMetricsController/GetLeftMetricsByPeriod params:\n" +
                $"fromTime: {fromTime},\n" +
                $"toTime: {toTime}");

            var hddMetricsResponse = new HddMetricsResponse
            {
                Metrics = _hddMetricRepository.GetByTimePeriod(fromTime, toTime)
                .Select(metric => _mapper.Map<HddMetricDto>(metric)).ToList()
            };

            return Ok(hddMetricsResponse);
        }

        [HttpGet("all")]
        public ActionResult<HddMetricsResponse> GetAllHddMetrics()
        {
            var hddMetricsReponse = new HddMetricsResponse
            {
                Metrics = _hddMetricRepository.GetAll()
                .Select(metric => _mapper.Map<HddMetricDto>(metric)).ToList()
            };
            return Ok(hddMetricsReponse);
        }
    }
}
