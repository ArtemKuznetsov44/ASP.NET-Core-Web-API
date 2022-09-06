using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerTests
    {
        private readonly CpuMetricsController _controller; 
<<<<<<< HEAD
        private 
=======
>>>>>>> 82c1144b80e48698a5950e4a80dd8d4719588fbc
        public CpuMetricsControllerTests() => _controller = new CpuMetricsController();

        [Fact]
        public void GetMetricsByPeriod_ReturnsOk()
        {
            var result = _controller.GetMetricsByPeriod(TimeSpan.Zero, TimeSpan.Zero);
            Assert.IsAssignableFrom<IActionResult>(result); 
        }
    }
}