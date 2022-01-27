using System.Collections.Generic;

namespace MetricsAgent.Interfaces;

public interface IRepository<T> where T : class
{
    IList<T> GetAll();
    T? GetById(int id);
    void Create(T item);
    void Update(T item);
    void Delete(int id);
    void CreateTestData();
    IList<T> GetByTimePeriod(DateTime from, DateTime to);
}
