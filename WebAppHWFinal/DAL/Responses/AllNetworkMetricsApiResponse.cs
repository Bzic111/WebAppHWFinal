using MetricsManager.DAL.DTO;

namespace MetricsManager.DAL.Responses;

public class AllNetworkMetricsApiResponse
{
    List<NetworkMetricDto> Metrics { get; set; }
}
