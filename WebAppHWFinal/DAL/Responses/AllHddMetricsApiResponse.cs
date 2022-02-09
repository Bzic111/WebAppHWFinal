using MetricsManager.DAL.DTO;

namespace MetricsManager.DAL.Responses;

public class AllHddMetricsApiResponse
{
    List<HddMetricDto> Metrics { get; set; }
}
