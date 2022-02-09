using MetricsManager.DAL.Requests;
using MetricsManager.DAL.Responses;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Interfaces;

public interface IMetricsAgentClient
{
    AllRamMetricsApiResponse? GetAllRamMetrics(GetAllRamMetricsApiRequest request);
    AllHddMetricsApiResponse? GetAllHddMetrics(GetAllHddMetricsApiRequest request);
    AllDonNetMetricsApiResponse? GetDonNetMetrics(GetDonNetHeapMetrisApiRequest request);
    AllCpuMetricsApiResponse? GetCpuMetrics(GetAllCpuMetricsApiRequest request);
}
