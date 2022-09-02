using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests
{
    public class RamMetricsControllerTests
    {
        private readonly RamMetricsController _controller;
        public RamMetricsControllerTests() => _controller = new RamMetricsController();

        [Fact]
        public void GetAvailableMetricByPeriod_ReturnsOk()
        {
            var result = _controller.GetAvailableMetricByPeriod(TimeSpan.Zero, TimeSpan.Zero);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}