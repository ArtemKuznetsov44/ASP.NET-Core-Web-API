using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
<<<<<<< HEAD
        #region Services

        private readonly ILogger<CpuMetricsController> _logger;

        #endregion

        public CpuMetricsController(ILogger<CpuMetricsController> logger) => _logger = logger;
     
=======
        // Пропишем методы, которые будут отдавать ответы пользователю.
        // Для остальных контроллеров, нужно сделать то же самое.

        // Мы знаем, что нам потребуется отдавать метрики по CPU для менеджера метрик, который будет
        // работать со множеством агентов, их собирающих.

        // Известно также, что для метрик предусмотрен
        // некоторый временной диапазон, в течение которого человек может их просмотреть.

        // Таким образом, нам потребуются методы, которые будут отдавать метрики В ЗАДАННОМ ДИАПАЗОНЕ ВРЕМЕНИ
        // С ОПРЕДЕЛЕННОГО АГЕНТА и метрики В УКАЗАННОМ ПЕРИОДЕ СО ВСЕХ АГЕНТОВ.

>>>>>>> 82c1144b80e48698a5950e4a80dd8d4719588fbc
        // Метод для получения метрик с одного определенного агента в указанном диапазоне времени: 
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
<<<<<<< HEAD
            _logger.LogInformation($"MetricsManager/CpuMetricsController/GetMetricsFromAgent params:\n" +
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
            _logger.LogInformation($"MetricsManager/CpuMetricsController/GetMetricsFromAllCluster params:\n" +
                $"fromTime: {fromTime},\n" +
                $"toTime: {toTime}");
=======
>>>>>>> 82c1144b80e48698a5950e4a80dd8d4719588fbc
            return Ok(); 
        }
    }
}
