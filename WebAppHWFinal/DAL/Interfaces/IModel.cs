namespace MetricsManager.DAL.Interfaces;

public interface IModel
{
    int Id { get; set; }
    int AgentId { get; set; }
    int Value { get; set; }
    DateTime Time { get; set; }
}