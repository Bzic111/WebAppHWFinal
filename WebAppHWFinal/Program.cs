using NLog.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
try
{
    logger.Debug("init main");
    CreateHostBuilder(args).Build().Run();
}
catch (Exception exception)                                                     // ����� ���� ���������� � ������ ������ ����������
{
    //NLog: ������������� ����� ����������
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // ��������� ������
    NLog.LogManager.Shutdown();
}
static IHostBuilder CreateHostBuilder(string[] args) =>
Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
{
    webBuilder.UseStartup<IStartup>();
})
.ConfigureLogging(logging =>
{
    logging.ClearProviders();                                                   // �������� ����������� �����������
    logging.SetMinimumLevel(LogLevel.Trace);                                    // ������������� ����������� ������� �����������
}).UseNLog();

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
