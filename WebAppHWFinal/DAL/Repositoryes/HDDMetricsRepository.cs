using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System.Globalization;

namespace MetricsManager.DAL.Repositoryes;

public class HDDMetricsRepository : IHddMetricsRepository
{
    private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
    private CultureInfo Culture = CultureInfo.GetCultureInfo("ru-RU");

    #region Create

    public void Create(HddMetric item, int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"INSERT INTO hddmetrics(AgentId, Value, Time) VALUES({agentId}, {item.Value}, \'{item.Time}\')");
        }
    }

    #endregion

    #region Read
    public IList<HddMetric> GetAll()
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.Query<HddMetric>("SELECT Id, AgentId, Time, Value FROM hddmetrics").ToList();
        }
    }

    public HddMetric? GetById(int id, int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.QuerySingle<HddMetric>($"SELECT Id, AgentId, Time, Value FROM hddmetrics WHERE id = {id} AND AgentId = {agentId}");
        }
    }

    public IList<HddMetric> GetByTimePeriod(DateTime from, DateTime to)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            string fromStr = from.ToString("s", Culture);
            string toStr = to.ToString("s", Culture);
            return connection.Query<HddMetric>($"SELECT Id, AgentId,Time, Value FROM hddmetrics WHERE Time >= \'{fromStr}\' AND Time <= \'{toStr}\'").ToList();
        }
    }
    public IList<HddMetric> GetAllByAgentId(int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.Query<HddMetric>($"SELECT Id,AgentId, Time, Value FROM hddmetrics WHERE AgentId = {agentId}").ToList();
        }
    }

    public IList<HddMetric> GetByAgentIdFilteredByTime(DateTime from, DateTime to, int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            string fromStr = from.ToString("s", Culture);
            string toStr = to.ToString("s", Culture);
            return connection.Query<HddMetric>($"SELECT Id, AgentId,Time, Value FROM hddmetrics WHERE AgentId = {agentId} AND Time BETWEEN \'{fromStr}\' and \'{toStr}\'").ToList();
        }
    }

    #endregion

    #region Update
    public void Update(HddMetric item, int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"UPDATE hddmetrics SET value = {item.Value}, time = \'{item.Time}\' WHERE id = {item.Id} AND AgentId = {agentId}");
        }
    }
    #endregion

    #region Delete
    public void Delete(int id, int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"DELETE FROM hddmetrics WHERE id={id} AND AgentId = {agentId}");
        }
    }

    #endregion
}