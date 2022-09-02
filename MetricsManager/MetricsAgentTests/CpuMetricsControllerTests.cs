using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerTests
    {
        private readonly CpuMetricsController _controller; 
        public CpuMetricsControllerTests() => _controller = new CpuMetricsController();

        [Fact]
        public void GetMetricsByPeriod_ReturnsOk()
        {
            var result = _controller.GetMetricsByPeriod(TimeSpan.Zero, TimeSpan.Zero);
            Assert.IsAssignableFrom<IActionResult>(result); 
        }
    }
}