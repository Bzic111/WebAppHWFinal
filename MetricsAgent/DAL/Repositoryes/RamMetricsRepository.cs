namespace MetricsAgent.DAL.Repositoryes;

public class RamMetricsRepository : IRamMetricsRepository
{
    private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
    private CultureInfo Culture = CultureInfo.GetCultureInfo("ru-RU");

    #region Create

    public void Create(RamMetric item)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"INSERT INTO rammetrics(value, time) VALUES({item.Value}, \'{item.Time}\'");
        }
    }

    #endregion

    #region Read

    public RamMetric? GetById(int id)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.QuerySingle<RamMetric>($"SELECT Id, Time, Value FROM rammetrics WHERE id = {id}");
        }
    }

    public IList<RamMetric> GetAll()
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.Query<RamMetric>("SELECT Id, Time, Value FROM rammetrics").ToList();
        }

    }

    public IList<RamMetric> GetByTimePeriod(DateTime from, DateTime to)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            string fromStr = from.ToString("s", Culture);
            string toStr = to.ToString("s", Culture);
            return connection.Query<RamMetric>($"SELECT Id, Time, Value FROM rammetrics WHERE Time >= \'{fromStr}\' AND Time <= \'{toStr}\'").ToList();
        }
    }

    #endregion

    #region Update

    public void Update(RamMetric item)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"UPDATE rammetrics SET value = {item.Value}, time = \'{item.Time}\' WHERE id = {item.Id}");
        }
    }

    #endregion

    #region Delete

    public void Delete(int id)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"DELETE FROM rammetrics WHERE id={id}");
        }
    }

    #endregion
}
