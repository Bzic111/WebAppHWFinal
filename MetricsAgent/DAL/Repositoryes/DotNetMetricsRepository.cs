namespace MetricsAgent.DAL.Repositoryes;

public class DotNetMetricsRepository : IDotNetRepository
{
    private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
    private CultureInfo Culture = CultureInfo.GetCultureInfo("ru-RU");

    #region Create

    public void Create(DotNetMetric item)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"INSERT INTO dotnetmetrics(value, time) VALUES({item.Value}, \'{item.Time}\'");
        }
    }

    #endregion

    #region Read

    public DotNetMetric? GetById(int id)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.QuerySingle<DotNetMetric>($"SELECT Id, Time, Value FROM dotnetmetrics WHERE id = {id}");
        }
    }

    public IList<DotNetMetric> GetAll()
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.Query<DotNetMetric>("SELECT Id, Time, Value FROM dotnetmetrics").ToList();
        }

    }

    public IList<DotNetMetric> GetByTimePeriod(DateTime from, DateTime to)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {

            string fromStr = from.ToString("s", Culture);// CultureInfo.GetCultureInfo("ru-RU"));
            string toStr = to.ToString("s", Culture);//CultureInfo.GetCultureInfo("ru-RU"));
            return connection.Query<DotNetMetric>($"SELECT Id, Time, Value FROM dotnetmetrics WHERE Time >= \'{fromStr}\' AND Time <= \'{toStr}\'").ToList();
        }
    }

    #endregion

    #region Update

    public void Update(DotNetMetric item)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"UPDATE dotnetmetrics SET value = {item.Value}, time = \'{item.Time}\' WHERE id = {item.Id}");
        }
    }

    #endregion

    #region Delete

    public void Delete(int id)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"DELETE FROM dotnetmetrics WHERE id={id}");
        }
    }

    #endregion
}
