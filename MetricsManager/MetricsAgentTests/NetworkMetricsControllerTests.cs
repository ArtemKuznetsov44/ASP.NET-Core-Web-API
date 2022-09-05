using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests
{
    public class NetworkMetricsControllerTests
    {
        private readonly NetworkMetricsController _controller;
        public NetworkMetricsControllerTests() => _controller = new NetworkMetricsController();

        [Fact]
        public void GetMetricsByPeriod_ReturnsOk()
        {
            var result = _controller.GetMetricsByPeriod(TimeSpan.Zero, TimeSpan.Zero);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
