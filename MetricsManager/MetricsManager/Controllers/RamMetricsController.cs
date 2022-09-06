using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
<<<<<<< HEAD
        #region Services

        private readonly ILogger<RamMetricsController> _logger;

        #endregion

        public RamMetricsController(ILogger<RamMetricsController> logger) => _logger = logger;

=======
>>>>>>> 82c1144b80e48698a5950e4a80dd8d4719588fbc
        // Метод для получения метрик с одного определенного агента в указанном диапазоне времени: 
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
<<<<<<< HEAD
            _logger.LogInformation($"MetricsManager/RamMetricsController/GetMetricsFromAgent params:\n" +
            $"agentId: {agentId},\n" +
            $"fromTime: {fromTime},\n" +
            $"toTime: {toTime}");
=======
>>>>>>> 82c1144b80e48698a5950e4a80dd8d4719588fbc
            return Ok();
        }

        // Метод для получения метрик со всех агентов в указанном диапазоне времени:
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
<<<<<<< HEAD
            _logger.LogInformation($"MetricsManager/RamMetricsController/GetMetricsFromAllCluster params:\n" +
             $"fromTime: {fromTime},\n" +
             $"toTime: {toTime}");
=======
>>>>>>> 82c1144b80e48698a5950e4a80dd8d4719588fbc
            return Ok();
        }
    }
}
