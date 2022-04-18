namespace MetricsAgent.DAL.Models;

public class RamMetric : IModel
{
    public int Id { get; set; }
    public int Value { get; set; }
    public DateTime Time { get; set; }
}
