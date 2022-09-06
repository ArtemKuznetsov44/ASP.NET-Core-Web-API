using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using Microsoft.Extensions.Logging;
=======
>>>>>>> 82c1144b80e48698a5950e4a80dd8d4719588fbc

namespace MetricsManagerTests
{
    public class CpuMetricsControllerTests
    {
        private readonly CpuMetricsController _controller;
<<<<<<< HEAD
        private readonly ILogger<CpuMetricsController> _logger;

        public CpuMetricsControllerTests() => _controller = new CpuMetricsController(_logger);
=======

        public CpuMetricsControllerTests() => _controller = new CpuMetricsController();
>>>>>>> 82c1144b80e48698a5950e4a80dd8d4719588fbc

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