namespace MetricsManager.DAL.Interfaces
{
    public class IRequest
    {
        DateTime FromTime { get; set; }
        DateTime ToTime { get; set; }
        Uri ClientBaseAddress { get; set; }
    }
}
