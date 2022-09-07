using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        #region Services

        private readonly AgentsRepository _agents;
        private readonly ILogger<AgentsController> _logger;

        #endregion

        public AgentsController(AgentsRepository agents, ILogger<AgentsController> logger)
        {
            _agents = agents;
            _logger = logger;
        }

        // Метод для регистрации/добавления агента сбора метрик:
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _logger.LogInformation($"MetricsManager/AgentsController/RegisterAgent params:\n" +
                $"agentInfo: {agentInfo}");
            return Ok(); 
        }
        
        // Метод для включение агента сбора метрик:
        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"MetricsManager/AgentsController/EnableAgentById params:\n" +
               $"agentInfo: {agentId}");
            return Ok(); 
        }

        // Метод для отключения агента сбора метрик:
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"MetricsManager/AgentsController/DisableAgentById params:\n" +
               $"agentInfo: {agentId}");
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
