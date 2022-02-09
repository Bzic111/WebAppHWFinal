using MetricsManager.DAL.Interfaces;
namespace MetricsManager.DAL.Requests;

public class GetAllCpuMetricsApiRequest : IRequest
{
    public DateTime FromTime { get; set; }
    public DateTime ToTime { get; set; }
    public Uri ClientBaseAddress { get; set; }
}
