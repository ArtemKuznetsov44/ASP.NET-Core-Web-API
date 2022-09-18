using MetricsManager.Models.DTO;
using MetricsManager.Models.Responses;
using MetricsManager.Services;
using MetricsManager.Services.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        #region Services

        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAgentsRepository _agentsRepository;
        private readonly IMetricsAgentClient _metricsAgentClient;

        #endregion

        public NetworkMetricsController(
            ILogger<NetworkMetricsController> logger,
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
        public ActionResult<NetworkMetricsResponse> GetMetricsFromAgent(
            [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsManager/NetworkMetricsController/GetMetricsFromAgent params:\n" +
             $"agentId: {agentId},\n" +
             $"fromTime: {fromTime},\n" +
             $"toTime: {toTime}");

            var metrics = _metricsAgentClient.GetNetworkMetrics(new Models.Requests.NetworkMetricsRequest
            {
                AgentId = agentId,
                FromTime = fromTime,
                ToTime = toTime
            }); 

            return Ok(metrics);
        }

        #region Previous method:

        //// Метод для получения метрик с одного определенного агента в указанном диапазоне времени: 
        //[HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        //public IActionResult GetMetricsFromAgent(
        //    [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        //{
        //    _logger.LogInformation($"MetricsManager/NetworkMetricsController/GetMetricsFromAgent params:\n" +
        //      $"agentId: {agentId},\n" +
        //      $"fromTime: {fromTime},\n" +
        //      $"toTime: {toTime}");

        //    AgentInfoDto agent = _agentsRepository.GetById(agentId); // Get a object of AgentInfo.

        //    if (agent is null) return BadRequest(); // Null verification.

        //    // Configure a specified request-string:
        //    string requestString =
        //       $"{agent.AgentUri}api/metrics/network/from/{fromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{toTime.ToString("dd\\.hh\\:mm\\:ss")}";

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

        //        NetworkMetricsResponse networkMetricsResponse =
        //           (NetworkMetricsResponse)JsonConvert.DeserializeObject(responseString, typeof(NetworkMetricsResponse));

        //        networkMetricsResponse.AgentId = agentId;

        //        return Ok(networkMetricsResponse);
        //    }
        //    return BadRequest();
        //}

        #endregion

        // Метод для получения метрик со всех агентов в указанном диапазоне времени:
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"MetricsManager/NetworkMetricsController/GetMetricsFromAllCluster params:\n" +
             $"fromTime: {fromTime},\n" +
             $"toTime: {toTime}");
            return Ok();
        }
    }
}
