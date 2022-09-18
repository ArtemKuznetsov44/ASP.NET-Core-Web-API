using MetricsManager.Models.DTO;
using MetricsManager.Models.Responses;
using MetricsManager.Services;
using MetricsManager.Services.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        #region Services

        private readonly ILogger<HddMetricsController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAgentsRepository _agentsRepository;
        private readonly IMetricsAgentClient _metricsAgentClient;

        #endregion

        public HddMetricsController(
            ILogger<HddMetricsController> logger,
            IAgentsRepository agentsRepository,
            IHttpClientFactory httpClientFactory, 
            IMetricsAgentClient metricsAgentClient)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _agentsRepository = agentsRepository;
            _metricsAgentClient = metricsAgentClient; 
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public ActionResult<HddMetricsResponse> GetMetricsFromAgent(
            [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsManager/HddMetricsController/GetMetricsFromAgent params:\n" +
               $"agentId: {agentId},\n" +
               $"fromTime: {fromTime},\n" +
               $"toTime: {toTime}");

            var metrics = _metricsAgentClient.GetHddMetrics(new Models.Requests.HddMetricsRequest
            {
                AgentId = agentId,
                FromTime = fromTime,
                ToTime = toTime
            }); 

            return Ok(metrics);
        }

        #region Previouse method:

        //// Метод для получения метрик с одного определенного агента в указанном диапазоне времени: 
        //[HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        //public IActionResult GetMetricsFromAgent(
        //    [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        //{
        //    _logger.LogInformation($"MetricsManager/HddMetricsController/GetMetricsFromAgent params:\n" +
        //       $"agentId: {agentId},\n" +
        //       $"fromTime: {fromTime},\n" +
        //       $"toTime: {toTime}");

        //    AgentInfoDto agent = _agentsRepository.GetById(agentId); // Get a object of AgentInfo.

        //    if (agent is null) return BadRequest(); // Null verification.

        //    // Configure a specified request-string:
        //    string requestString =
        //       $"{agent.AgentUri}api/metrics/hdd/from/{fromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{toTime.ToString("dd\\.hh\\:mm\\:ss")}";

        //    // First param is a type of our controller method:
        //    HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestString);
        //    httpRequestMessage.Headers.Add("Accept", "application/json"); // Adding some headers.

        //    //Create a http client object:
        //    HttpClient httpClient = _httpClientFactory.CreateClient();

        //    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        //    cancellationTokenSource.CancelAfter(3000); // After 3 seconds.

        //    // This method will return a result of our request, but 
        //    // it must do it in no more than 3 seconds, otherwise the response will not be successful:
        //    HttpResponseMessage response = httpClient.Send(httpRequestMessage, cancellationTokenSource.Token);
        //    // If response is successful:
        //    if (response.IsSuccessStatusCode)
        //    {
        //        // Get a result string with our metrics from a response.Content:
        //        string responseString = response.Content.ReadAsStringAsync().Result;

        //        HddMetricsResponse hddMetricsResponse =
        //           (HddMetricsResponse)JsonConvert.DeserializeObject(responseString, typeof(HddMetricsResponse));

        //        hddMetricsResponse.AgentId = agentId;

        //        return Ok(hddMetricsResponse);
        //    }
        //    return BadRequest();
        //}

        #endregion

        // Метод для получения метрик со всех агентов в указанном диапазоне времени:
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsManager/HddMetricsController/GetMetricsFromAllCluster params:\n" +
              $"fromTime: {fromTime},\n" +
              $"toTime: {toTime}");
            return Ok();
        }
    }
}
