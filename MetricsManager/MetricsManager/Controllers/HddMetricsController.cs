using MetricsManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        #region Services

        private readonly ILogger<HddMetricsController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAgentsRepository _agentsRepository;

        #endregion

        public HddMetricsController(
            ILogger<HddMetricsController> logger,
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
            _logger.LogInformation($"MetricsManager/HddMetricsController/GetMetricsFromAgent params:\n" +
               $"agentId: {agentId},\n" +
               $"fromTime: {fromTime},\n" +
               $"toTime: {toTime}");
            return Ok();
        }

        // Метод для получения метрик со всех агентов в указанном диапазоне времени:
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsManager/HddMetricsController/GetMetricsFromAllCluster params:\n" +
              $"fromTime: {fromTime},\n" +
              $"toTime: {toTime}");
            return Ok();
        }
    }
}
