namespace MetricsAgent.DAL.Requests;

public class CpuMetricCreateRequest
{
    public DateTime Time { get; set; }
    public int Value { get; set; }
}
