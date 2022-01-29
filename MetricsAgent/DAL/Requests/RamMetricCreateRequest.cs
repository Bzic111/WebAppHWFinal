namespace MetricsAgent.DAL.Requests;

public class RamMetricCreateRequest
{
    public DateTime Time { get; set; }
    public int Value { get; set; }
}
