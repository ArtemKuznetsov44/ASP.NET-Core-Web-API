using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests
{
    public class DotNetMetricsControllerTests
    {
        private readonly DotNetMetricsController _controller;
        public DotNetMetricsControllerTests() => _controller = new DotNetMetricsController();

        [Fact]
        public void GetErrorsCountMetricByPeriod_ReturnsOk()
        {
            var result = _controller.GetErrorsCountMetricByPeriod(TimeSpan.Zero, TimeSpan.Zero);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
