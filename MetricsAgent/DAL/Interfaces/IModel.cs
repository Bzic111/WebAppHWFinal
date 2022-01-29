namespace MetricsAgent.DAL.Interfaces;

public interface IModel
{
    int Id { get; set; }
    int Value { get; set; }
    DateTime Time { get; set; }
}