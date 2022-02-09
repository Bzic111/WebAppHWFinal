using MetricsManager.DAL.Responses;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.DAL.DTO;
using System.Globalization;

namespace MetricsManager.DAL.Repositoryes;

public class CPUMetricsRepository : ICpuMetricsRepository
{
    private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
    private CultureInfo Culture = CultureInfo.GetCultureInfo("ru-RU");

    #region Create

    public void Create(CpuMetric item, int agentId)
    {
        using(var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"INSERT INTO cpumetrics(AgentId, Value, Time) VALUES({agentId}, {item.Value}, \'{item.Time}\')");
        }
    }

    #endregion

    #region Read
    public IList<CpuMetric> GetAll()
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.Query<CpuMetric>("SELECT Id, AgentId, Time, Value FROM cpumetrics").ToList();
        }
    }

    public CpuMetric? GetById(int id, int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.QuerySingle<CpuMetric>($"SELECT Id, AgentId, Time, Value FROM cpumetrics WHERE id = {id} AND AgentId = {agentId}");
        }
    }

    public IList<CpuMetric> GetByTimePeriod(DateTime from, DateTime to)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            string fromStr = from.ToString("s", Culture);
            string toStr = to.ToString("s", Culture);
            return connection.Query<CpuMetric>($"SELECT Id, AgentId,Time, Value FROM cpumetrics WHERE Time >= \'{fromStr}\' AND Time <= \'{toStr}\'").ToList();
        }
    }
    public IList<CpuMetric> GetAllByAgentId(int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.Query<CpuMetric>($"SELECT Id,AgentId, Time, Value FROM cpumetrics WHERE AgentId = {agentId}").ToList();
        }
    }

    public IList<CpuMetric> GetByAgentIdFilteredByTime(DateTime from, DateTime to, int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            string fromStr = from.ToString("s", Culture);
            string toStr = to.ToString("s", Culture);
            return connection.Query<CpuMetric>($"SELECT Id, AgentId,Time, Value FROM cpumetrics WHERE AgentId = {agentId} AND Time BETWEEN \'{fromStr}\' and \'{toStr}\'").ToList();
        }
    }

    #endregion

    #region Update
    public void Update(CpuMetric item, int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"UPDATE cpumetrics SET value = {item.Value}, time = \'{item.Time}\' WHERE id = {item.Id} AND AgentId = {agentId}");
        }
    }
    #endregion

    #region Delete
    public void Delete(int id, int agentId)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"DELETE FROM cpumetrics WHERE id={id} AND AgentId = {agentId}");
        }
    }
        
    #endregion
}
