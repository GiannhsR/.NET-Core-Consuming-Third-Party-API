using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TestCore.DTO;
using Newtonsoft.Json;

namespace TestCore.Services
{
    public class IMatchHistoryService
    {
        private const string MatchHistoryByAccountId = "/lol/match/v4/matchlists/by-account/";
        private readonly IRetrieveApiKeyService _retrieveApiKeyService;
        private readonly IHttpClientFactory _httpClientFactory;
        public IMatchHistoryService(IRetrieveApiKeyService retrieveApiKeyService, IHttpClientFactory httpClientFactory)
        {
            _retrieveApiKeyService = retrieveApiKeyService;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<MatchHistoryDTO> MatchHistoryByAccountIdAndRegionAsync(string accountId, string selectedRegion)
        {
            var uri = String.Concat("https://", selectedRegion, MatchHistoryByAccountId, accountId, "?api_key=", _retrieveApiKeyService.RetrieveApiKey());
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<MatchHistoryDTO>(content);
        }
    }
}
