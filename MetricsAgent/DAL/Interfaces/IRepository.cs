namespace MetricsAgent.DAL.Interfaces;

public interface IRepository<T> where T : class
{
    void Create(T item);
    void Update(T item);
    void Delete(int id);
    T? GetById(int id);
    IList<T> GetAll();
    IList<T> GetByTimePeriod(DateTime from, DateTime to);
}
