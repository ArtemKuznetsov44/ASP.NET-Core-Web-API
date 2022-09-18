using AutoMapper;
using MetricsManager.Models;
using MetricsManager.Models.DTO;
using MetricsManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        #region Services

        private readonly IAgentsRepository _agentsRepository;
        private readonly ILogger<AgentsController> _logger;
        private readonly IMapper _mapper;

        #endregion

        public AgentsController(
            ILogger<AgentsController> logger, 
            IAgentsRepository agentsRepository, 
            IMapper mapper)
        {
            _agentsRepository = agentsRepository;
            _logger = logger;
            _mapper = mapper;
            
        }

        // Метод для регистрации/добавления агента сбора метрик:
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfoDto agentInfoDto)
        {
            _logger.LogInformation($"MetricsManager/AgentsController/RegisterAgent params:\n" +
                $"agentInfo: {agentInfoDto}");

            _agentsRepository.Add(agentInfoDto);  

            return Ok(); 
        }
        
        // Метод для включение агента сбора метрик:
        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"MetricsManager/AgentsController/EnableAgentById params:\n" +
               $"agentInfo: {agentId}");

            _agentsRepository.Enable(agentId);

            return Ok(); 
        }

        // Метод для отключения агента сбора метрик:
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"MetricsManager/AgentsController/DisableAgentById params:\n" +
               $"agentInfo: {agentId}");

            _agentsRepository.Disable(agentId); 

            return Ok(); 
        }

        // Метод для получения всех зарегистрированных агентов:
        [HttpGet("registeredAgents")]
        public ActionResult<AgentInfoDto> GetAllRegisteredAgents() => 
            Ok(_agentsRepository.GetAll().ToArray());
      
    }
}
