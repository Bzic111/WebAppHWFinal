﻿namespace MetricsAgent.DAL.Models;

public class DotNetMetric : IModel
{
    public int Id { get; set; }
    public int Value { get; set; }
    public DateTime Time { get; set; }
}