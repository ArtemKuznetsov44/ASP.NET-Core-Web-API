using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests
{
    public class HddMetricsControllerTests
    {
        private readonly HddMetricsController _controller;
        public HddMetricsControllerTests() => _controller = new HddMetricsController();

        [Fact]
        public void GetLeftMetricsByPeriod_ReturnsOk()
        {
            var result = _controller.GetLeftMetricsByPeriod(TimeSpan.Zero, TimeSpan.Zero);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
