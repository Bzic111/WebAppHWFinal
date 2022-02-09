using MetricsManager.DAL.DTO;

namespace MetricsManager.DAL.Responses;

public class AllDonNetMetricsApiResponse
{
    List<DotNetMetricDto> Metrics { get; set; }
}
