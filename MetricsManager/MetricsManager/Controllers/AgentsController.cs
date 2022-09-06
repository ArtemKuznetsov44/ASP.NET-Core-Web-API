using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
<<<<<<< HEAD
        #region Services

        private readonly AgentsRepository _agents;
        private readonly ILogger<AgentsController> _logger;

        #endregion

        public AgentsController(AgentsRepository agents, ILogger<AgentsController> logger)
        {
            _agents = agents;
            _logger = logger;
        }
=======

        private readonly AgentsRepository _agents;

        public AgentsController(AgentsRepository agents) => _agents = agents; 
>>>>>>> 82c1144b80e48698a5950e4a80dd8d4719588fbc

        // Метод для регистрации/добавления агента сбора метрик:
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
<<<<<<< HEAD
            _logger.LogInformation($"MetricsManager/AgentsController/RegisterAgent params:\n" +
                $"agentInfo: {agentInfo}");
=======
>>>>>>> 82c1144b80e48698a5950e4a80dd8d4719588fbc
            return Ok(); 
        }
        
        // Метод для включение агента сбора метрик:
        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
<<<<<<< HEAD
            _logger.LogInformation($"MetricsManager/AgentsController/EnableAgentById params:\n" +
               $"agentInfo: {agentId}");
=======
>>>>>>> 82c1144b80e48698a5950e4a80dd8d4719588fbc
            return Ok(); 
        }

        // Метод для отключения агента сбора метрик:
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
<<<<<<< HEAD
            _logger.LogInformation($"MetricsManager/AgentsController/DisableAgentById params:\n" +
               $"agentInfo: {agentId}");
=======
>>>>>>> 82c1144b80e48698a5950e4a80dd8d4719588fbc
            return Ok(); 
        }
        
        // Метод для получения всех зарегистрированных агентов:
        [HttpGet("registeredAgents")]
        public IActionResult GetAllRegisteredAgents()
        {
            return Ok(_agents); 
        }
    }
}
