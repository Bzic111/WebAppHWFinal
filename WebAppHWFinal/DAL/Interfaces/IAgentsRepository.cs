using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Interfaces;

public interface IAgentsRepository
{
    void Create(Uri uri);
    Agent GetById(int id);
    Agent GetByUri(Uri uri);
    List<Agent> GetAll();
    bool DeleteById(int id);
    bool DeleteByUri(Uri uri);
    bool UpdateAgent(Agent agent);
    bool UpdateById(int id, Agent agent);
}
