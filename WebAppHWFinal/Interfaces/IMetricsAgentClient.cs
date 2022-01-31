
using MetricsManager.Requests;
using MetricsManager.Responses;

namespace MetricsManager.Interfaces;

public interface IMetricsAgentClient
{
    AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request);
    AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest    request);
    DonNetMetricsApiResponse GetDonNetMetrics(DonNetHeapMetrisApiRequest    request);
    AllCpuMetricsApiResponse GetCpuMetrics(GetAllCpuMetricsApiRequest request);
}
