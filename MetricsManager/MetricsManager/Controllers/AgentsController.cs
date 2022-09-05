using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {

        private readonly AgentsRepository _agents;

        public AgentsController(AgentsRepository agents) => _agents = agents; 

        // Метод для регистрации/добавления агента сбора метрик:
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            return Ok(); 
        }
        
        // Метод для включение агента сбора метрик:
        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            return Ok(); 
        }

        // Метод для отключения агента сбора метрик:
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
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
