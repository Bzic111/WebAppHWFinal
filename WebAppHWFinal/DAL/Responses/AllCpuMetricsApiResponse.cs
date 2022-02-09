using MetricsManager.DAL.DTO;

namespace MetricsManager.DAL.Responses;

public class AllCpuMetricsApiResponse
{
    List<CpuMetricDto> Metrics { get; set; }
}
