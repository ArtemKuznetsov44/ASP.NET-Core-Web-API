using MetricsManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        #region Services

        private readonly ILogger<RamMetricsController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAgentsRepository _agentsRepository;

        #endregion

        public RamMetricsController(
            ILogger<RamMetricsController> logger,
            IAgentsRepository agentsRepository,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _agentsRepository = agentsRepository;

        }

        // Метод для получения метрик с одного определенного агента в указанном диапазоне времени: 
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsManager/RamMetricsController/GetMetricsFromAgent params:\n" +
            $"agentId: {agentId},\n" +
            $"fromTime: {fromTime},\n" +
            $"toTime: {toTime}");
            return Ok();
        }

        // Метод для получения метрик со всех агентов в указанном диапазоне времени:
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsManager/RamMetricsController/GetMetricsFromAllCluster params:\n" +
             $"fromTime: {fromTime},\n" +
             $"toTime: {toTime}");
            return Ok();
        }
    }
}
