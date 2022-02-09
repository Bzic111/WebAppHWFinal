using MetricsManager.DAL.Models;
using System.Globalization;
using MetricsManager.DAL.Interfaces;

namespace MetricsManager.DAL.Repositoryes;

public class NetworkMetricsRepository : INetworkMetricsRepository
{
    private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
    private CultureInfo Culture = CultureInfo.GetCultureInfo("ru-RU");

    #region Create

    public void Create(NetworkMetric item, int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"INSERT INTO networkmetrics(AgentId, Value, Time) VALUES({agentId}, {item.Value}, \'{item.Time}\')");
        }
    }

    #endregion

    #region Read
    public IList<NetworkMetric> GetAll()
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.Query<NetworkMetric>("SELECT Id, AgentId, Time, Value FROM networkmetrics").ToList();
        }
    }

    public NetworkMetric? GetById(int id, int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.QuerySingle<NetworkMetric>($"SELECT Id, AgentId, Time, Value FROM networkmetrics WHERE id = {id} AND AgentId = {agentId}");
        }
    }

    public IList<NetworkMetric> GetByTimePeriod(DateTime from, DateTime to)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            string fromStr = from.ToString("s", Culture);
            string toStr = to.ToString("s", Culture);
            return connection.Query<NetworkMetric>($"SELECT Id, AgentId,Time, Value FROM networkmetrics WHERE Time >= \'{fromStr}\' AND Time <= \'{toStr}\'").ToList();
        }
    }
    public IList<NetworkMetric> GetAllByAgentId(int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.Query<NetworkMetric>($"SELECT Id,AgentId, Time, Value FROM networkmetrics WHERE AgentId = {agentId}").ToList();
        }
    }

    public IList<NetworkMetric> GetByAgentIdFilteredByTime(DateTime from, DateTime to, int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            string fromStr = from.ToString("s", Culture);
            string toStr = to.ToString("s", Culture);
            return connection.Query<NetworkMetric>($"SELECT Id, AgentId,Time, Value FROM networkmetrics WHERE AgentId = {agentId} AND Time BETWEEN \'{fromStr}\' and \'{toStr}\'").ToList();
        }
    }

    #endregion

    #region Update
    public void Update(NetworkMetric item, int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"UPDATE networkmetrics SET value = {item.Value}, time = \'{item.Time}\' WHERE id = {item.Id} AND AgentId = {agentId}");
        }
    }
    #endregion

    #region Delete
    public void Delete(int id, int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"DELETE FROM networkmetrics WHERE id={id} AND AgentId = {agentId}");
        }
    }

    #endregion
}