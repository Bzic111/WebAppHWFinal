using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using MetricsManager.Responses;
using MetricsManager.Requests;
using MetricsManager.Interfaces;

namespace MetricsManager
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        public AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request)
        {
            throw new NotImplementedException();
        }

        public AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request)
        {
            throw new NotImplementedException();
        }

        public AllCpuMetricsApiResponse GetCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            throw new NotImplementedException();
        }

        public DonNetMetricsApiResponse GetDonNetMetrics(DonNetHeapMetrisApiRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
