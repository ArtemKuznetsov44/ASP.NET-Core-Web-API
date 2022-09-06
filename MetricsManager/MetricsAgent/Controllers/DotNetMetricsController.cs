<<<<<<< HEAD
﻿using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
=======
﻿using Microsoft.AspNetCore.Http;
>>>>>>> 82c1144b80e48698a5950e4a80dd8d4719588fbc
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
<<<<<<< HEAD
        #region Services

        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IDotNetMetricsRepository _dotNetMetricsRepository;

        #endregion

        public DotNetMetricsController(
            ILogger<DotNetMetricsController> logger,
            IDotNetMetricsRepository dotNetMetricsRepository)
        {
            _logger = logger;
            _dotNetMetricsRepository = dotNetMetricsRepository;
        }

        // Method for creation DotNetMetric:
        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
        {
            _dotNetMetricsRepository.Create(new Models.DotNetMetric
            {
                Value = request.Value,
                Time = (int)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<DotNetMetric>> GetErrorsCountMetricByPeriod([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsAgent/DotNetMetricsController/GetErrorsCountMetricByPeriod params:\n" +
                $"fromTime: {fromTime},\n" +
                $"toTime: {toTime}");
            return Ok(_dotNetMetricsRepository.GetByTimePeriod(fromTime, toTime));
=======
        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCountMetricByPeriod([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok(); 
>>>>>>> 82c1144b80e48698a5950e4a80dd8d4719588fbc
        }
    }
}
