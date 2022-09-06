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
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
<<<<<<< HEAD
        #region Services

        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricsRepository _ramMetricsRepository;

        #endregion 

        public RamMetricsController(
            ILogger<RamMetricsController> logger,
            IRamMetricsRepository ramMetricsRepository)
        {
            _logger = logger;
            _ramMetricsRepository = ramMetricsRepository;
        }

        // Method for creation RamMetric:
        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _ramMetricsRepository.Create(new Models.RamMetric
            {
                Value = request.Value,
                Time = (int)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<RamMetric>> GetAvailableMetricByPeriod([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsAgent/RamMetricsController/GetAvailableMetricByPeriod params:\n" +
               $"fromTime: {fromTime},\n" +
               $"toTime: {toTime}");
            return Ok(_ramMetricsRepository.GetByTimePeriod(fromTime, toTime)); 
=======
        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetAvailableMetricByPeriod([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok(); 
>>>>>>> 82c1144b80e48698a5950e4a80dd8d4719588fbc
        }
    }
}
