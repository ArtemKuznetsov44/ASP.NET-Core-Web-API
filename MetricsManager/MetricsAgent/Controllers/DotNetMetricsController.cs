using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MetricsAgent.Models.DTO;

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
        public ActionResult<IList<DotNetMetricDto>> GetErrorsCountMetricByPeriod([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsAgent/DotNetMetricsController/GetErrorsCountMetricByPeriod params:\n" +
                $"fromTime: {fromTime},\n" +
                $"toTime: {toTime}");
            return Ok(_dotNetMetricsRepository.GetByTimePeriod(fromTime, toTime).
                Select(metric => _mapper.Map<DotNetMetricDto>(metric)).ToList());
        }

        [HttpGet("all")]
        public ActionResult<IList<DotNetMetricDto>> GetAllDotNetMetrics() => Ok(_dotNetMetricsRepository.GetAll()
            .Select(metric => _mapper.Map<DotNetMetricDto>(metric)).ToList());
    }
}
