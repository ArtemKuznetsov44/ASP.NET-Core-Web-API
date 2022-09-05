using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        #region Services

        private readonly ILogger<DotNetMetricsController> _logger;

        #endregion

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger) => _logger = logger;
     
        // Метод для получения метрик с одного определенного агента в указанном диапазоне времени: 
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsManager/DotNetMetricsController/GetMetricsFromAgent params:\n" +
                $"agentId: {agentId},\n" +
                $"fromTime: {fromTime},\n" +
                $"toTime: {toTime}");
            return Ok();
        }

        // Метод для получения метрик со всех агентов в указанном диапазоне времени:
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsManager/DotNetMetricsController/GetMetricsFromAllCluster params:\n" +
              $"fromTime: {fromTime},\n" +
              $"toTime: {toTime}");
            return Ok();
        }
    }
}
