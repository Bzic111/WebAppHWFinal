global using Dapper;
global using MetricsAgent.DAL.DTO;
global using MetricsAgent.DAL.Interfaces;
global using MetricsAgent.DAL.Models;
global using MetricsAgent.DAL.Repositoryes;
global using MetricsAgent.DAL.Requests;
global using MetricsAgent.DAL.Responses;
global using Microsoft.AspNetCore.Mvc;
global using NLog;
global using NLog.Web;
global using System.Data.SQLite;
global using System.Globalization;
global using AutoMapper;
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

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
    builder.Services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
    builder.Services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();
    builder.Services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
    builder.Services.AddScoped<IDotNetRepository, DotNetMetricsRepository>();
    builder.Services.AddScoped<INetworkRepository, NetworkMetricsRepository>();
    builder.Services.AddSingleton(mapper);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

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

void CreateTestTables()
{
    string[] tables =
               {
                "cpumetrics",
                "dotnetmetrics",
                "hddmetrics",
                "networkmetrics",
                "rammetrics"
            };
    foreach (var item in tables)
    {
        using (var connection = new SQLiteConnection("Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;"))
        {
            connection.Open();
            using (var cmd = new SQLiteCommand(connection))
            {

                cmd.CommandText = $"DROP TABLE IF EXISTS {item};";
                cmd.ExecuteNonQuery();
                cmd.CommandText = $@"CREATE TABLE {item}(id INTEGER PRIMARY KEY, value INT, time TEXT)";
                cmd.ExecuteNonQuery();
                for (int i = 0; i < 10; i++)
                {
                    string temp = $"INSERT INTO {item}(value, time) VALUES({(i + 10) * 2},\'{DateTime.Now.ToString("s",CultureInfo.GetCultureInfo("ru-RU"))}\')";
                    cmd.CommandText = temp;
                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
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