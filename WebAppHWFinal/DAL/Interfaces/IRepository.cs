namespace MetricsManager.DAL.Interfaces;

public interface IRepository<T> where T : class
{
    void Create(T item, int agentId);
    void Update(T item, int agentId);
    void Delete(int id, int agentId);
    T? GetById(int id, int agentId);
    IList<T> GetAll();
    IList<T> GetAllByAgentId(int agentId);
    IList<T> GetByTimePeriod(DateTime from, DateTime to);
    IList<T> GetByAgentIdFilteredByTime(DateTime from, DateTime to, int agentId);
}
