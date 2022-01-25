using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using System.Data.SQLite;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Repositoryes;

public class NetworkMetricsRepository : INetworkRepository
{
    private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
    public void Create(NetworkMetric item)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (var cmd = new SQLiteCommand(connection))
            {
                cmd.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(@value,@time)";
                cmd.Parameters.AddWithValue("@value", item.Value);
                cmd.Parameters.AddWithValue("@time", item.Time.Ticks);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Delete(int id)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (var cmd = new SQLiteCommand(connection))
            {
                cmd.CommandText = "DELETE FROM networkmetrics WHERE id=@id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public IList<NetworkMetric> GetAll()
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (var cmd = new SQLiteCommand(connection))
            {
                cmd.CommandText = "SELECT * FROM networkmetrics";
                var returnList = new List<NetworkMetric>();
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnList.Add(new NetworkMetric
                        {
                            Id = reader.GetInt32(0),
                            Value = reader.GetInt32(1),
                            Time = DateTime.Now - TimeSpan.FromSeconds(reader.GetInt32(2))
                        });
                    }
                    return returnList;
                }
            }
        }

    }

    public NetworkMetric? GetById(int id)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (var cmd = new SQLiteCommand(connection))
            {
                cmd.CommandText = "SELECT * FROM networkmetrics WHERE id=@id";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new NetworkMetric
                        {
                            Id = reader.GetInt32(0),
                            Value = reader.GetInt32(1),
                            Time = DateTime.Now - TimeSpan.FromSeconds(reader.GetInt32(1))
                        };
                    }
                    return null;
                }
            }
        }
    }

    public void Update(NetworkMetric item)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (var cmd = new SQLiteCommand(connection))
            {
                cmd.CommandText = "UPDATE networkmetrics SET value = @value, time = @time WHERE id = @id; ";
                cmd.Parameters.AddWithValue("@id", item.Id);
                cmd.Parameters.AddWithValue("@value", item.Value);
                cmd.Parameters.AddWithValue("@time", item.Time.Ticks);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
