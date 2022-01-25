using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using System.Data.SQLite;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Repositoryes;

public class RamMetricsRepository : IRamMetricsRepository
{
    private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
    public void Create(RamMetric item)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (var cmd = new SQLiteCommand(connection))
            {
                cmd.CommandText = "INSERT INTO rammetrics(value, time) VALUES(@value,@time)";
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
                cmd.CommandText = "DELETE FROM rammetrics WHERE id=@id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public IList<RamMetric> GetAll()
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (var cmd = new SQLiteCommand(connection))
            {
                cmd.CommandText = "SELECT * FROM rammetrics";
                var returnList = new List<RamMetric>();
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnList.Add(new RamMetric
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

    public RamMetric? GetById(int id)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (var cmd = new SQLiteCommand(connection))
            {
                cmd.CommandText = "SELECT * FROM rammetrics WHERE id=@id";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new RamMetric
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

    public void Update(RamMetric item)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (var cmd = new SQLiteCommand(connection))
            {
                cmd.CommandText = "UPDATE rammetrics SET value = @value, time = @time WHERE id = @id; ";
                cmd.Parameters.AddWithValue("@id", item.Id);
                cmd.Parameters.AddWithValue("@value", item.Value);
                cmd.Parameters.AddWithValue("@time", item.Time.Ticks);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
