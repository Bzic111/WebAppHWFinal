﻿using MetricsManager.DAL.Interfaces;
namespace MetricsManager.DAL.DTO;

public class RamMetricDto : IMetricDto
{
    public DateTime Time { get; set; }
    public int Value { get; set; }
    public int Id { get; set; }
    public int AgentId { get; set; }
}
