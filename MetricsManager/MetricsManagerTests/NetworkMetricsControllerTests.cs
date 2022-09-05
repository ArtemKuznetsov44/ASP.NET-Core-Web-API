using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManagerTests
{
    public class NetworkMetricsControllerTests
    {
        private readonly NetworkMetricsController _controller;
        private ILogger<NetworkMetricsController> _logger;

        public NetworkMetricsControllerTests() => _controller = new NetworkMetricsController(_logger);

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
