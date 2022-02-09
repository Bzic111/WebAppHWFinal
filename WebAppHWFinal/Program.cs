global using AutoMapper;
global using Microsoft.AspNetCore.Mvc;
global using NLog;
global using NLog.Web;
global using System.Data.SQLite;
global using Dapper;

using MetricsManager;
using MetricsManager.DAL.Interfaces;
using Polly;
using System.Text;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");


try
{
    var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
    var mapper = mapperConfiguration.CreateMapper();
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    ConfigureSqlLiteConnection(builder.Services);
    CreateTestTables();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpClient();
    builder.Services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>().AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));


    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)                                                     // отлов всех исключений в рамках работы приложения
{
    //NLog: устанавливаем отлов исключений
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally                                                                         // остановка логера
{
    NLog.LogManager.Shutdown();
}

void ConfigureSqlLiteConnection(IServiceCollection services)
{
    const string connectionString = "Data Source = metrics.db; Version = 3; Pooling = true; Max Pool Size = 100; ";
    var connection = new SQLiteConnection(connectionString);
    connection.Open();
    PrepareSchema(connection);
}
void PrepareSchema(SQLiteConnection connection)
{
    using (var command = new SQLiteCommand(connection))
    {
        command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
        command.ExecuteNonQuery();
        command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY, value INT, time INT)";
        command.ExecuteNonQuery();
    }
}

void CreateTable(string name,string[] fields,string[] fieldTypes)
{
    StringBuilder sb = new StringBuilder();
    sb.Append($"CREATE TABLE {name}(");
    for (int i = 0; i < fields.Length; i++)
        sb.Append($", {fields[i]} {fieldTypes[i]}");
    sb.Append(")");
    using(var connection = new SQLiteConnection("Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;"))
    {
        connection.Execute(sb.ToString());
    }
}
void DropTable(string name)
{
    using (var connection = new SQLiteConnection("Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;"))
    {
        connection.Execute($"DROP TABLE IF EXISTS {name};");
    }
}
void CreateTestTables()
{
    string[] cpumetrics = { "cpumetrics","id","agentId","value","time" };
    string[] dotnetmetrics = { "dotnetmetrics", "id", "agentId", "value", "time" };
    string[] hddmetrics = { "hddmetrics", "id", "agentId", "value", "time" };
    string[] networkmetrics = { "networkmetrics", "id", "agentId", "value", "time" };
    string[] rammetrics = { "rammetrics", "id", "agentId", "value", "time" };
    string[] agents = { "Agents", "id", "uri","enabled" };

    string[][] tables = { cpumetrics, dotnetmetrics, hddmetrics, networkmetrics, rammetrics, agents };

    foreach (string[] item in tables)
    {
        string cmdText = "";
        using (var connection = new SQLiteConnection("Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;"))
        {
            using (var cmd = new SQLiteCommand(connection))
            {
                connection.Execute($"DROP TABLE IF EXISTS {item[0]};");
                switch (item.Length)
                {
                    case 5: 
                        cmdText += $"CREATE TABLE {item[0]}({item[1]} INTEGER PRIMARY KEY, {item[2]} INT, {item[3]} INT, {item[4]} TEXT)";
                        break;
                    case 4:
                        cmdText += $"CREATE TABLE {item[0]}({item[1]} INTEGER PRIMARY KEY, {item[2]} TEXT, {item[3]} BOOL)";
                        break;
                    default:
                        break;
                }
                connection.Execute(cmdText);
            }
        }
    }
}