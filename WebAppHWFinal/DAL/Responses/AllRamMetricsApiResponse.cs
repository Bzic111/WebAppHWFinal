using MetricsManager.DAL.DTO;

namespace MetricsManager.DAL.Responses;

public class AllRamMetricsApiResponse
{
    List<RamMetricDto> Metrics { get; set; }
}