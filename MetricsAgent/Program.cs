using NLog.Web;
using MetricsAgent.Interfaces;
using MetricsAgent.Repositoryes;
using System.Data.SQLite;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using MetricsAgent;
using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Other;
//using MetricsAgent.DAL;




var builder = WebApplication.CreateBuilder(args);
var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
try
{
    logger.Debug("init main");
    CreateHostBuilder(args).Build().Run();
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
static IHostBuilder CreateHostBuilder(string[] args) =>
Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
{
    webBuilder.UseStartup<IStartup>();
})
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();                                               // создание провайдеров логирования
        logging.SetMinimumLevel(LogLevel.Trace);                                // устанавливаем минимальный уровень логирования
    }).UseNLog();


builder.Services.AddControllers();
ConfigureSqlLiteConnection(builder.Services);
builder.Services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
builder.Services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();
builder.Services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
builder.Services.AddScoped<IDotNetRepository, DotNetMetricsRepository>();
builder.Services.AddScoped<INetworkRepository, INetworkRepository>();

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