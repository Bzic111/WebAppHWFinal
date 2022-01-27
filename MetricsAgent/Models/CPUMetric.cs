using MetricsAgent.Interfaces;
namespace MetricsAgent.Models;

public class CpuMetric : IModel
{
    public int Id { get; set; }
    public int Value { get; set; }
    public DateTime Time { get; set; }
}
