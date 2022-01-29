﻿namespace MetricsAgent.DAL.Repositoryes;

public class HddMetricsRepository : IHddMetricsRepository
{
    private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
    private CultureInfo Culture = CultureInfo.GetCultureInfo("ru-RU");

    #region Create

    public void Create(HddMetric item)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"INSERT INTO hddmetrics(value, time) VALUES({item.Value}, \'{item.Time}\'");
        }
    }

    #endregion

    #region Read
    public HddMetric? GetById(int id)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.QuerySingle<HddMetric>($"SELECT Id, Time, Value FROM hddmetrics WHERE id = {id}");
        }
    }

    public IList<HddMetric> GetAll()
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.Query<HddMetric>("SELECT Id, Time, Value FROM hddmetrics").ToList();
        }
    }

    public IList<HddMetric> GetByTimePeriod(DateTime from, DateTime to)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            string fromStr = from.ToString("s", Culture);
            string toStr = to.ToString("s", Culture);
            return connection.Query<HddMetric>($"SELECT Id, Time, Value FROM hddmetrics WHERE Time >= \'{fromStr}\' AND Time <= \'{toStr}\'").ToList();
        }
    }

    #endregion

    #region Update

    public void Update(HddMetric item)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"UPDATE hddmetrics SET value = {item.Value}, time = \'{item.Time}\' WHERE id = {item.Id}");
        }
    }

    #endregion

    #region Delete

    public void Delete(int id)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"DELETE FROM hddmetrics WHERE id={id}");
        }
    }

    #endregion
}