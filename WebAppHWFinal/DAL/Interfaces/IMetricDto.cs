namespace MetricsManager.DAL.Interfaces;

public interface IMetricDto
{
    DateTime Time { get; set; }
    int Value { get; set; }
    int Id { get; set; }
    int AgentId { get; set; }
}
