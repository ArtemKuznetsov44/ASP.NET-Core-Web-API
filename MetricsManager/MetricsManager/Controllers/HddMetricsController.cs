using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
<<<<<<< HEAD
        #region Services

        private readonly ILogger<HddMetricsController> _logger;

        #endregion

        public HddMetricsController(ILogger<HddMetricsController> logger) => _logger = logger;
      
=======
>>>>>>> 82c1144b80e48698a5950e4a80dd8d4719588fbc
        // Метод для получения метрик с одного определенного агента в указанном диапазоне времени: 
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
<<<<<<< HEAD
            _logger.LogInformation($"MetricsManager/HddMetricsController/GetMetricsFromAgent params:\n" +
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
            _logger.LogInformation($"MetricsManager/HddMetricsController/GetMetricsFromAllCluster params:\n" +
              $"fromTime: {fromTime},\n" +
              $"toTime: {toTime}");
=======
>>>>>>> 82c1144b80e48698a5950e4a80dd8d4719588fbc
            return Ok();
        }
    }
}
