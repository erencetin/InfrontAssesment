using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;
using InfrontAssesment.Core.Models;
using InfrontAssesment.Core.Interfaces;

namespace InfrontAssesment.Infrastructure.Repositories
{
    public class PriceDataRepository: IPriceDataRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PriceDataRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<PriceData> GetPriceData(string vwdKey)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var newRequest = new HttpRequestMessage(HttpMethod.Get,
                $"https://test.solutions.vwdservices.com/internal/intake-test/sample-data/price-data/{vwdKey}");
            var response = await httpClient.SendAsync(newRequest, CancellationToken.None);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PriceData>();
        }
    }
}
