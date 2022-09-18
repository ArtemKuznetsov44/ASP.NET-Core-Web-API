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
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        #region Services:

        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricsRepository _ramMetricsRepository;
        private readonly IMapper _mapper;

        #endregion

        public RamMetricsController(
            ILogger<RamMetricsController> logger,
            IRamMetricsRepository ramMetricsRepository, 
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _ramMetricsRepository = ramMetricsRepository;
        }

        // Method for creation RamMetric:
        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _ramMetricsRepository.Create(_mapper.Map<RamMetric>(request)); 
            return Ok();
        }

        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public ActionResult<RamMetricsResponse> GetAvailableMetricByPeriod(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsAgent/RamMetricsController/GetAvailableMetricByPeriod params:\n" +
               $"fromTime: {fromTime},\n" +
               $"toTime: {toTime}");

            var ramMetricsResponse = new RamMetricsResponse
            {
                Metrics = _ramMetricsRepository.GetByTimePeriod(fromTime, toTime)
                .Select(metric => _mapper.Map<RamMetricDto>(metric)).ToList()
            }; 

            return Ok(ramMetricsResponse); 
        }

        [HttpGet("all")]
        public ActionResult<RamMetricsResponse> GetAllRamMetrics()
        {
            var ramMetricsResponse = new RamMetricsResponse
            {
                Metrics = _ramMetricsRepository.GetAll()
            .Select(metric => _mapper.Map<RamMetricDto>(metric)).ToList()
            }; 

            return Ok(ramMetricsResponse);
        }
    }
}
