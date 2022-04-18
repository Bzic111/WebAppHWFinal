namespace MetricsAgent.DAL.Repositoryes;

public class CpuMetricsRepository : ICpuMetricsRepository
{
    private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
    private CultureInfo Culture = CultureInfo.GetCultureInfo("ru-RU");

    #region Create

    public void Create(CpuMetric item)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"INSERT INTO cpumetrics(value, time) VALUES({item.Value}, \'{item.Time}\'");
        }
    }

    #endregion

    #region Read

    public CpuMetric? GetById(int id)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.QuerySingle<CpuMetric>($"SELECT Id, Time, Value FROM cpumetrics WHERE id = {id}");
        }
    }

    public IList<CpuMetric> GetAll()
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.Query<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics").ToList();
        }
    }

    public IList<CpuMetric> GetByTimePeriod(DateTime from, DateTime to)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            string fromStr = from.ToString("s", Culture);
            string toStr = to.ToString("s", Culture);
            return connection.Query<CpuMetric>($"SELECT Id, Time, Value FROM cpumetrics WHERE Time >= \'{fromStr}\' AND Time <= \'{toStr}\'").ToList();
        }
    }

    #endregion

    #region Update

    public void Update(CpuMetric item)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"UPDATE cpumetrics SET value = {item.Value}, time = \'{item.Time}\' WHERE id = {item.Id}");
        }

    }

    #endregion

    #region Delete

    public void Delete(int id)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"DELETE FROM cpumetrics WHERE id={id}");
        }
    }

    #endregion
}
