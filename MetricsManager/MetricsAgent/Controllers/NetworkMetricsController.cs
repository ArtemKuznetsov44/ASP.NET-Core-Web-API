using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MetricsAgent.Models.DTO;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        #region Services:

        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _networkMetricsRepository;
        private readonly IMapper _mapper;

        #endregion

        public NetworkMetricsController(
            ILogger<NetworkMetricsController> logger,
            INetworkMetricsRepository networkMetricsRepository, 
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _networkMetricsRepository = networkMetricsRepository;
        }

        // Method for creation NetworkMetric:
        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            _networkMetricsRepository.Create(_mapper.Map<NetworkMetric>(request)); 
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<NetworkMetricDto>> GetMetricsByPeriod([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsAgent/NetworkMetricsController/GetMetricsByPeriod params:\n" +
               $"fromTime: {fromTime},\n" +
               $"toTime: {toTime}");
            return Ok(_networkMetricsRepository.GetByTimePeriod(fromTime, toTime)
                .Select(metric => _mapper.Map<NetworkMetricDto>(metric)).ToList());
        }

        [HttpGet("all")]
        public ActionResult<IList<NetworkMetricDto>> GetAllNetworkMetrics() => Ok(_networkMetricsRepository.GetAll()
            .Select(metric => _mapper.Map<NetworkMetricDto>(metric)).ToList());
    }
}
