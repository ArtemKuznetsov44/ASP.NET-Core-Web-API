using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerTests
{
    public class AgentsControllerTests
    {
        private readonly AgentsController _controller;
        public AgentsControllerTests() => _controller = new AgentsController(new MetricsManager.AgentsRepository());

        [Fact]
        public void RegisterAgent()
        {
            var result = _controller.RegisterAgent(new MetricsManager.AgentInfo());
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void EnableAgent()
        {
            var result = _controller.EnableAgentById(1);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void DisableAgent()
        {
            var result = _controller.DisableAgentById(1);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
