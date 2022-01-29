namespace MetricsAgent.DAL.Repositoryes;

public class NetworkMetricsRepository : INetworkRepository
{
    private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
    private CultureInfo Culture = CultureInfo.GetCultureInfo("ru-RU");
    #region Create

    public void Create(NetworkMetric item)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"INSERT INTO networkmetrics(value, time) VALUES({item.Value}, \'{item.Time}\'");
        }
    }

    #endregion

    #region Read

    public NetworkMetric? GetById(int id)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.QuerySingle<NetworkMetric>($"SELECT Id, Time, Value FROM networkmetrics WHERE id = {id}");
        }
    }

    public IList<NetworkMetric> GetAll()
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.Query<NetworkMetric>("SELECT Id, Time, Value FROM networkmetrics").ToList();
        }

    }

    public IList<NetworkMetric> GetByTimePeriod(DateTime from, DateTime to)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            string fromStr = from.ToString("s", Culture);
            string toStr = to.ToString("s", Culture);
            return connection.Query<NetworkMetric>($"SELECT Id, Time, Value FROM networkmetrics WHERE Time >= \'{fromStr}\' AND Time <= \'{toStr}\'").ToList();
        }
    }
    #endregion

    #region Update

    public void Update(NetworkMetric item)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"UPDATE networkmetrics SET value = {item.Value}, time = \'{item.Time}\' WHERE id = {item.Id}");
        }
    }

    #endregion

    #region Delete

    public void Delete(int id)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"DELETE FROM networkmetrics WHERE id={id}");
        }
    }

    #endregion
}
