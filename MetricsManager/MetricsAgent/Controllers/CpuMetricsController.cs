using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.DTO;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        #region Services:

        // Logger type should be CpuMetricsController because its work in it.
        private readonly ILogger<CpuMetricsController> _logger; // Adding logger field.
        private readonly ICpuMetricsRepository _cpuMetricsRepository;
        private readonly IMapper _mapper; 

        #endregion 
        
        public CpuMetricsController(
            ILogger<CpuMetricsController> logger, 
            ICpuMetricsRepository cpuMetricsRepository, 
            IMapper mapper)
        {
            _logger = logger; // Intialize our logger in constructor.
            _mapper = mapper;
            _cpuMetricsRepository = cpuMetricsRepository;
        }
        
        // Method for creating CpuMetric:
        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            #region Variant_1:

            //_cpuMetricsRepository.Create(new Models.CpuMetric
            //{
            //    Value = request.Value,
            //    Time = (int)request.Time.TotalSeconds
            //});

            #endregion

            #region Variant_2.AutoMapper:

            _cpuMetricsRepository.Create(_mapper.Map<CpuMetric>(request));

            #endregion

            return Ok();
        }

        // Method for getting CpuMetric-s objects by period:
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<CpuMetricDto>> GetCpuMetricsByPeriod([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsAgent/CpuMetricsController/GetMetricsByPeriod params:\n" +
                $"fromTime: {fromTime},\n" +
                $"toTime: {toTime}");

            #region Variant_1:

            //List<CpuMetricDto> list = new List<CpuMetricDto>(); 
            //foreach(var metric in _cpuMetricsRepository.GetByTimePeriod(fromTime, toTime).ToList())
            //{
            //    list.Add(new CpuMetricDto
            //    {
            //        Value = metric.Value,
            //        Time = metric.Time
            //    }); 
            //}

            #endregion

            #region Variant_2:

            //var list = _cpuMetricsRepository.GetByTimePeriod(fromTime, toTime).Select(metric => new
            //CpuMetricDto
            //{
            //    Time = metric.Time, 
            //    Value = metric.Value
            //}).ToList();

            #endregion

            #region Variant_3.AutoMapper:

            return Ok(_cpuMetricsRepository.GetByTimePeriod(fromTime, toTime)
                .Select(metric => _mapper.Map<CpuMetricDto>(metric)).ToList()); 

            #endregion 

        }

        // Method returns a list with all CpuMetric-s objects which were created before:
        [HttpGet("all")]
        public ActionResult<IList<CpuMetric>> GetAllCpuMetrics() => Ok(_cpuMetricsRepository.GetAll()
            .Select(metric => _mapper.Map<CpuMetricDto>(metric)).ToList()); 
    }
}
