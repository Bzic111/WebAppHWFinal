using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using MetricsAgent.Other;

namespace MetricsAgent.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SQLTestController : ControllerBase
{
    [HttpGet("sql-test")]
    public IActionResult TryToSqlLite()
    {
        string cs = "Data Source=:memory:";
        string stm = "SELECT SQLITE_VERSION()";
        using (var con = new SQLiteConnection(cs))
        {
            con.Open();
            using var cmd = new SQLiteCommand(stm, con);
            string version = cmd.ExecuteScalar().ToString();
            return Ok(version);
        }
    }

    [HttpGet("sql-read-write-test")]
    public IActionResult TryToInsertAndRead()
    {
        string connectionString = "Data Source=:memory:";
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY, value INT, time INT)";
                command.ExecuteNonQuery();
                for (int i = 0; i < 4; i++)
                {
                    command.CommandText = @$"INSERT INTO cpumetrics(value, time) VALUES({(i + 10)*2},{(i + 2)*3})";
                    command.ExecuteNonQuery();
                }
                string readQuery = "SELECT * FROM cpumetrics LIMIT 3";
                var returnArray = new CpuMetric[3];
                command.CommandText = readQuery;
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    var counter = 0;
                    while (reader.Read())
                    {
                        returnArray[counter] = new CpuMetric
                        {
                            Id = reader.GetInt32(0),
                            Value = reader.GetInt32(1),
                            Time = reader.GetInt64(2)
                        };
                        counter++;
                    }
                }
                return Ok(returnArray);
            }
        }
    }

}
