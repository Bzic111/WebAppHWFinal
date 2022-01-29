﻿using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using System.Data.SQLite;
using System.Globalization;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Repositoryes;

public class DotNetMetricsRepository : IDotNetRepository
{
    private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

    public void Create(DotNetMetric item)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (var cmd = new SQLiteCommand(connection))
            {
                cmd.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(@value,@time)";
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
                cmd.CommandText = "DELETE FROM dotnetmetrics WHERE id=@id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public IList<DotNetMetric> GetAll()
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (var cmd = new SQLiteCommand(connection))
            {
                cmd.CommandText = "SELECT * FROM dotnetmetrics";
                var returnList = new List<DotNetMetric>();
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnList.Add(new DotNetMetric
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

    public DotNetMetric? GetById(int id)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (var cmd = new SQLiteCommand(connection))
            {
                cmd.CommandText = "SELECT * FROM dotnetmetrics WHERE id=@id";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new DotNetMetric
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

    public void Update(DotNetMetric item)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (var cmd = new SQLiteCommand(connection))
            {
                cmd.CommandText = "UPDATE dotnetmetrics SET value = @value, time = @time WHERE id = @id; ";
                cmd.Parameters.AddWithValue("@id", item.Id);
                cmd.Parameters.AddWithValue("@value", item.Value);
                cmd.Parameters.AddWithValue("@time", item.Time.Ticks);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void CreateTestData()
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (var cmd = new SQLiteCommand(connection))
            {
                cmd.CommandText = "DROP TABLE IF EXISTS dotnetmetrics";
                cmd.ExecuteNonQuery();
                cmd.CommandText = @"CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY, value INT, time INTEGER)";
                cmd.ExecuteNonQuery();
                for (int i = 0; i < 10; i++)
                {
                    cmd.CommandText = $"INSERT INTO dotnetmetrics(value, time) VALUES({(i + 10) * 2},{(i + 2) * 3}";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
    public IList<DotNetMetric> GetByTimePeriod(DateTime from, DateTime to)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            using (var cmd = new SQLiteCommand(connection))
            {
                string fromStr = from.ToString("s", CultureInfo.GetCultureInfo("ru-RU"));
                string toStr = to.ToString("s", CultureInfo.GetCultureInfo("ru-RU"));
                cmd.CommandText = $"SELECT * FROM dotnetmetrics WHERE Time >= \'{fromStr}\' AND Time <= \'{toStr}\'";
                cmd.Prepare();
                var returnList = new List<DotNetMetric>();
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnList.Add(new DotNetMetric
                        {
                            Id = reader.GetInt32(0),
                            Value = reader.GetInt32(1),
                            Time = DateTime.Now - TimeSpan.FromSeconds(reader.GetInt32(2))
                        });
                    }
                }
                return returnList;
            }
        }
    }
}
