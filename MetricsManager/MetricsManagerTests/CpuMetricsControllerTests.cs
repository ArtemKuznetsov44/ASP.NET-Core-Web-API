using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManagerTests
{
    public class CpuMetricsControllerTests
    {
        private readonly CpuMetricsController _controller;
        private readonly ILogger<CpuMetricsController> _logger;

        public CpuMetricsControllerTests() => _controller = new CpuMetricsController(_logger);

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            var result = _controller.GetMetricsFromAgent(1, TimeSpan.Zero, TimeSpan.Zero);
            Assert.IsAssignableFrom<IActionResult>(result); 
        }

        [Fact]
        public void GetMetricsFromAllCluster_ReturnsOk()
        {
            var result = _controller.GetMetricsFromAllCluster(TimeSpan.Zero, TimeSpan.Zero);
            Assert.IsAssignableFrom<IActionResult>(result); 
        }
    }
}