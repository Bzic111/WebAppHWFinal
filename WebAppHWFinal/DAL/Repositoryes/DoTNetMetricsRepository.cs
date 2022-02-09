using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System.Globalization;

namespace MetricsManager.DAL.Repositoryes;

public class DoTNetMetricsRepository : IDotNetMetricsRepository
{
    private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
    private CultureInfo Culture = CultureInfo.GetCultureInfo("ru-RU");

    #region Create

    public void Create(DotNetMetric item, int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"INSERT INTO dotnetmetrics(AgentId, Value, Time) VALUES({agentId}, {item.Value}, \'{item.Time}\')");
        }
    }

    #endregion

    #region Read
    public IList<DotNetMetric> GetAll()
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.Query<DotNetMetric>("SELECT Id, AgentId, Time, Value FROM dotnetmetrics").ToList();
        }
    }

    public DotNetMetric? GetById(int id, int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.QuerySingle<DotNetMetric>($"SELECT Id, AgentId, Time, Value FROM dotnetmetrics WHERE id = {id} AND AgentId = {agentId}");
        }
    }

    public IList<DotNetMetric> GetByTimePeriod(DateTime from, DateTime to)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            string fromStr = from.ToString("s", Culture);
            string toStr = to.ToString("s", Culture);
            return connection.Query<DotNetMetric>($"SELECT Id, AgentId,Time, Value FROM dotnetmetrics WHERE Time >= \'{fromStr}\' AND Time <= \'{toStr}\'").ToList();
        }
    }
    public IList<DotNetMetric> GetAllByAgentId(int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.Query<DotNetMetric>($"SELECT Id,AgentId, Time, Value FROM dotnetmetrics WHERE AgentId = {agentId}").ToList();
        }
    }

    public IList<DotNetMetric> GetByAgentIdFilteredByTime(DateTime from, DateTime to, int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            string fromStr = from.ToString("s", Culture);
            string toStr = to.ToString("s", Culture);
            return connection.Query<DotNetMetric>($"SELECT Id, AgentId,Time, Value FROM dotnetmetrics WHERE AgentId = {agentId} AND Time BETWEEN \'{fromStr}\' and \'{toStr}\'").ToList();
        }
    }

    #endregion

    #region Update
    public void Update(DotNetMetric item, int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"UPDATE dotnetmetrics SET value = {item.Value}, time = \'{item.Time}\' WHERE id = {item.Id} AND AgentId = {agentId}");
        }
    }
    #endregion

    #region Delete
    public void Delete(int id, int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"DELETE FROM dotnetmetrics WHERE id={id} AND AgentId = {agentId}");
        }
    }

    #endregion
}