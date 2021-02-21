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

        private readonly IConfiguration _config;

        private readonly IHttpClientFactory _httpClientFactory;

        public IMatchHistoryService(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }
        
        public string RetrieveApiKey()
        {
            return _config.GetValue<string>("API_KEY");
        }
        public async Task<MatchHistoryDTO> MatchHistoryByAccountIdAsync(string accountId) {
            var uri = String.Concat("https://","eun1.api.riotgames.com",MatchHistoryByAccountId,accountId,"?api_key=",RetrieveApiKey());
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<MatchHistoryDTO>(content);

        }
    }
}
