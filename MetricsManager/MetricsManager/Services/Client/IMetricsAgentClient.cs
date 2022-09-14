using MetricsManager.Models.Requests;
using MetricsManager.Models.Responses;

namespace MetricsManager.Services.Client
{
    public interface IMetricsAgentClient
    {
        CpuMetricsResponse GetCpuMetrics(CpuMetricsRequest request); 
        DotNetMetricsResponse GetDotNetMetrics(DotNetMetricsRequest request);
        HddMetricsResponse GetHddMetrics(HddMetricsRequest request);
        NetworkMetricsResponse GetNetworkMetrics(NetworkMetricsRequest request);
        RamMetricsResponse GetRamMetrcis(RamMetricsRequest); 
    }
}
