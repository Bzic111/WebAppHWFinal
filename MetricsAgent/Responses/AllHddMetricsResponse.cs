using MetricsAgent.DTO;

namespace MetricsAgent.Responses;

public class AllHddMetricsResponse
{
    public List<HddMetricDto>? Metrics { get; set; }
}
