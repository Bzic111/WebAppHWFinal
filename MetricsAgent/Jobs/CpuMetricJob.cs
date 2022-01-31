using MetricsAgent.DAL;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System.Diagnostics;
using System.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Specialized;

namespace MetricsAgent.Jobs;

public class CpuMetricJob : IJob
{
    private ICpuMetricsRepository _repository;
    private PerformanceCounter _cpuCounter;

    public CpuMetricJob(ICpuMetricsRepository repository)
    {
        _repository = repository;
        _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
    }

    public Task Execute(IJobExecutionContext context)
    {
        var cpuUsageInPercents = Convert.ToInt32(_cpuCounter.NextValue());
        var time = DateTime.Now;
        _repository.Create(new CpuMetric
        {
            Time = time,
            Value = cpuUsageInPercents
        });
        return Task.CompletedTask;
    }
}
public class RamMetricJob : IJob
{
    private IRamMetricsRepository _repository;
    public RamMetricJob(IRamMetricsRepository rep)
    {
        _repository = rep;
    }
    public Task Execute(IJobExecutionContext context)
    {
        return Task.CompletedTask;
    }
}
