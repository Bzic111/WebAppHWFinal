namespace MetricsAgent.DAL.Responses;

public class AllDotNetMetricsResponse
{
    public List<DotNetMetricDto>? Metrics { get; set; }
}