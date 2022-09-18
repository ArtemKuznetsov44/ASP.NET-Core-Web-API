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
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        #region Services:

        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IDotNetMetricsRepository _dotNetMetricsRepository;
        private readonly IMapper _mapper;

        #endregion

        public DotNetMetricsController(
            ILogger<DotNetMetricsController> logger,
            IDotNetMetricsRepository dotNetMetricsRepository,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _dotNetMetricsRepository = dotNetMetricsRepository;
        }

        // Method for creation DotNetMetric:
        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
        {
            _dotNetMetricsRepository.Create(_mapper.Map<DotNetMetric>(request));
            return Ok();
        }

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public ActionResult<DotNetMetricsResponse> GetErrorsCountMetricByPeriod(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsAgent/DotNetMetricsController/GetErrorsCountMetricByPeriod params:\n" +
                $"fromTime: {fromTime},\n" +
                $"toTime: {toTime}");

            var dotNetMetricsResponse = new DotNetMetricsResponse
            {
                Metrics = _dotNetMetricsRepository.GetByTimePeriod(fromTime, toTime).
                Select(metric => _mapper.Map<DotNetMetricDto>(metric)).ToList()
            };

            return Ok(dotNetMetricsResponse);
        }

        [HttpGet("all")]
        public ActionResult<DotNetMetricsResponse> GetAllDotNetMetrics()
        {
            var dotNetMetricsResponse = new DotNetMetricsResponse
            {
                Metrics = _dotNetMetricsRepository.GetAll()
                .Select(metric => _mapper.Map<DotNetMetricDto>(metric)).ToList()
            };
            return Ok(dotNetMetricsResponse);
        }
    }
}
