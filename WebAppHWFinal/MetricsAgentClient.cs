using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Requests;
using MetricsManager.DAL.Responses;
using System.Text.Json;
using ILogger = Microsoft.Extensions.Logging.ILogger;
//using ILogger = NLog.ILogger;

namespace MetricsManager;

public class MetricsAgentClient : IMetricsAgentClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    public MetricsAgentClient(HttpClient httpClient, ILogger logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    public AllHddMetricsApiResponse? GetAllHddMetrics(GetAllHddMetricsApiRequest request)
    {
        string req = $"{request.ClientBaseAddress}/api/metrics/hdd/left/filter/{request.FromTime}/to/{request.ToTime}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, req); 
        try
        {
            using HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;//  SendAsync(httpRequest).Result;
            using var responseStream = response.Content.ReadAsStreamAsync().Result;
            return JsonSerializer.DeserializeAsync<AllHddMetricsApiResponse>(responseStream).Result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return null;
    }

    public AllRamMetricsApiResponse? GetAllRamMetrics(GetAllRamMetricsApiRequest request)
    {
        string req = $"{request.ClientBaseAddress}/api/metrics/ram/available/filter/{request.FromTime}/to/{request.ToTime}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, req);
        try
        {
            using (var response = _httpClient.SendAsync(httpRequest).Result)
            {
                using (var responseStream = response.Content.ReadAsStreamAsync().Result)
                {
                    return JsonSerializer.DeserializeAsync<AllRamMetricsApiResponse>(responseStream).Result;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return null;
    }

    public AllCpuMetricsApiResponse? GetCpuMetrics(GetAllCpuMetricsApiRequest request)
    {
        string req = $"{request.ClientBaseAddress}/api/metrics/cpu/usage/filter/{request.FromTime}/to/{request.ToTime}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, req); 
        try
        {
            using (var response = _httpClient.SendAsync(httpRequest).Result)
            {
                using (var responseStream = response.Content.ReadAsStreamAsync().Result)
                {
                    return JsonSerializer.DeserializeAsync<AllCpuMetricsApiResponse>(responseStream).Result;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return null;
    }
    public AllDonNetMetricsApiResponse? GetDonNetMetrics(GetDonNetHeapMetrisApiRequest request)
    {
        string req = $"{request.ClientBaseAddress}/api/metrics/hdd/left/filter/{request.FromTime}/to/{request.ToTime}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, req);
        try
        {
            using (var response = _httpClient.SendAsync(httpRequest).Result)
            {
                using (var responseStream = response.Content.ReadAsStreamAsync().Result)
                {
                    return JsonSerializer.DeserializeAsync<AllDonNetMetricsApiResponse>(responseStream).Result;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return null;
    }
    public AllNetworkMetricsApiResponse? GetNetworkMetrics(GetNetworkMetrisApiRequest request)
    {
        string req = $"{request.ClientBaseAddress}/api/metrics/network/filter/{request.FromTime}/to/{request.ToTime}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, req);
        try
        {
            using (var response = _httpClient.SendAsync(httpRequest).Result)
            {
                using (var responseStream = response.Content.ReadAsStreamAsync().Result)
                {
                    return JsonSerializer.DeserializeAsync<AllNetworkMetricsApiResponse>(responseStream).Result;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return null;
    }
}