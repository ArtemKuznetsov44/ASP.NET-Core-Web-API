using MetricsManager.Models.DTO;
using MetricsManager.Models.Requests;
using MetricsManager.Models.Responses;
using Newtonsoft.Json;

namespace MetricsManager.Services.Client.Implimintation
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        #region Services:

        private readonly IAgentsRepository _agentsRepository;
        private readonly HttpClient _httpClient;

        #endregion

        // We must work with HttpClient param.
        public MetricsAgentClient(HttpClient httpClient, IAgentsRepository agentsRepository)
        {
            _httpClient = httpClient; 
            _agentsRepository = agentsRepository;
        }

        public CpuMetricsResponse GetCpuMetrics(CpuMetricsRequest request)
        {
            AgentInfoDto agent = _agentsRepository.GetById(request.AgentId); // Get a object of AgentInfo.

            if (agent is null) return null; // Null verification.

            // Configure a specified request-string:
            string requestString =
               $"{agent.AgentUri}api/metrics/cpu/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";

            // First param is a type of our controller method:
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestString);
            httpRequestMessage.Headers.Add("Accept", "application/json"); // Adding some headers.

         
            // This method will return a result of our request:
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            // If response is successful:
            if (response.IsSuccessStatusCode)
            {
                // Get a result string with our metrics from a response.Content:
                string responseString = response.Content.ReadAsStringAsync().Result;
                CpuMetricsResponse cpuMetricsResponse =
                   (CpuMetricsResponse)JsonConvert.DeserializeObject(responseString, typeof(CpuMetricsResponse));
               
                cpuMetricsResponse.AgentId = request.AgentId;

                return (cpuMetricsResponse);
            }
            return null; 
        }

        public DotNetMetricsResponse GetDotNetMetrics(DotNetMetricsRequest request)
        {
            AgentInfoDto agent = _agentsRepository.GetById(request.AgentId); // Get a object of AgentInfo.

            if (agent is null) return null; // Null verification.

            // Configure a specified request-string:
            string requestString =
               $"{agent.AgentUri}api/metrics/dotnet/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";

            // First param is a type of our controller method:
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestString);
            httpRequestMessage.Headers.Add("Accept", "application/json"); // Adding some headers.


            // This method will return a result of our request:
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            // If response is successful:
            if (response.IsSuccessStatusCode)
            {
                // Get a result string with our metrics from a response.Content:
                string responseString = response.Content.ReadAsStringAsync().Result;

                DotNetMetricsResponse dotNetMetricsResponse =
                   (DotNetMetricsResponse)JsonConvert.DeserializeObject(responseString, typeof(DotNetMetricsResponse));

                dotNetMetricsResponse.AgentId = request.AgentId;

                return (dotNetMetricsResponse);
            }
            return null;
        }

        public HddMetricsResponse GetHddMetrics(HddMetricsRequest request)
        {
            AgentInfoDto agent = _agentsRepository.GetById(request.AgentId); // Get a object of AgentInfo.

            if (agent is null) return null; // Null verification.

            // Configure a specified request-string:
            string requestString =
               $"{agent.AgentUri}api/metrics/hdd/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";

            // First param is a type of our controller method:
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestString);
            httpRequestMessage.Headers.Add("Accept", "application/json"); // Adding some headers.


            // This method will return a result of our request:
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            // If response is successful:
            if (response.IsSuccessStatusCode)
            {
                // Get a result string with our metrics from a response.Content:
                string responseString = response.Content.ReadAsStringAsync().Result;

                HddMetricsResponse hddMetricsResponse =
                   (HddMetricsResponse)JsonConvert.DeserializeObject(responseString, typeof(HddMetricsResponse));

                hddMetricsResponse.AgentId = request.AgentId;

                return (hddMetricsResponse);
            }
            return null;
        }

        public NetworkMetricsResponse GetNetworkMetrics(NetworkMetricsRequest request)
        {
            AgentInfoDto agent = _agentsRepository.GetById(request.AgentId); // Get a object of AgentInfo.

            if (agent is null) return null; // Null verification.

            // Configure a specified request-string:
            string requestString =
               $"{agent.AgentUri}api/metrics/network/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";

            // First param is a type of our controller method:
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestString);
            httpRequestMessage.Headers.Add("Accept", "application/json"); // Adding some headers.


            // This method will return a result of our request:
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            // If response is successful:
            if (response.IsSuccessStatusCode)
            {
                // Get a result string with our metrics from a response.Content:
                string responseString = response.Content.ReadAsStringAsync().Result;

                NetworkMetricsResponse networkMetricsResponse =
                   (NetworkMetricsResponse)JsonConvert.DeserializeObject(responseString, typeof(NetworkMetricsResponse));

                networkMetricsResponse.AgentId = request.AgentId;

                return (networkMetricsResponse);
            }
            return null;
        }

        public RamMetricsResponse GetRamMetrcis(RamMetricsRequest request)
        {
            AgentInfoDto agent = _agentsRepository.GetById(request.AgentId); // Get a object of AgentInfo.

            if (agent is null) return null; // Null verification.

            // Configure a specified request-string:
            string requestString =
               $"{agent.AgentUri}api/metrics/ram/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";

            // First param is a type of our controller method:
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestString);
            httpRequestMessage.Headers.Add("Accept", "application/json"); // Adding some headers.


            // This method will return a result of our request:
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            // If response is successful:
            if (response.IsSuccessStatusCode)
            {
                // Get a result string with our metrics from a response.Content:
                string responseString = response.Content.ReadAsStringAsync().Result;

                RamMetricsResponse ramMetricsResponse =
                   (RamMetricsResponse)JsonConvert.DeserializeObject(responseString, typeof(RamMetricsResponse));

                ramMetricsResponse.AgentId = request.AgentId;

                return (ramMetricsResponse);
            }
            return null;
        }
    }
}
