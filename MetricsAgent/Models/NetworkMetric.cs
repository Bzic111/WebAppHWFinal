using MetricsAgent.Interfaces;
namespace MetricsAgent.Models;

public class NetworkMetric : IModel
{
    public int Id { get; set; }
    public int Value { get; set; }
    public DateTime Time { get; set; }
}