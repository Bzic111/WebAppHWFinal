﻿using MetricsManager.DAL.Interfaces;

namespace MetricsManager.DAL.Models;

public class CpuMetric : IModel
{
    public int Id { get; set; }
    public int Value { get; set; }
    public DateTime Time { get; set; }
    public int AgentId { get; set; }
}
