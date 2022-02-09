using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Repositoryes;

public class AgentsRepository : IAgentsRepository
{
    private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
    
    #region Create

    public void Create(Uri uri)
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"INSERT INTO Agents(uri) VALUES(\'{uri}\')");
        }
    }

    #endregion

    #region Read

    public Agent GetById(int id)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.QuerySingle<Agent>($"SELECT id, uri FROM Agents WHERE id = {id}");
        }
    }

    public Agent GetByUri(Uri uri)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.QuerySingle<Agent>($"SELECT id, uri FROM Agents WHERE uri = \'{uri}\'");
        }
    }

    public List<Agent> GetAll()
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            return connection.Query<Agent>("SELECT Id, uri FROM Agents").ToList();
        }
    }

    #endregion

    #region Update

    public bool UpdateAgent(Agent agent)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"UPDATE Agents SET uri = \'{agent.BaseUri}\' WHERE id = {agent.Id}");
            return true;
        }
    }

    public bool UpdateById(int id, Agent agent)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"UPDATE Agents SET uri = \'{agent.BaseUri}\' WHERE id = {id}");
            return true;
        }
    }

    #endregion

    #region Delete

    public bool DeleteById(int id)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"DELETE FROM Agents WHERE id={id}");
            return true;
        }
    }

    public bool DeleteByUri(Uri uri)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Execute($"DELETE FROM Agents WHERE uri={uri}");
            return true;
        }
    }

    #endregion

}
