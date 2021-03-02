using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCore.HelperClasses;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using TestCore.DTO;
namespace TestCore.Services
{
    public class ISearchSummonerService 
    {
        private const string SummonerByNameEndpoint = "/lol/summoner/v4/summoners/by-name/";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IRetrieveApiKeyService _retrieveApiKeyService;
        public ISearchSummonerService(IRetrieveApiKeyService retrieveApiKeyService, IHttpClientFactory httpClientFactory)
        {
            _retrieveApiKeyService = retrieveApiKeyService;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<SummonerDTO> SearchSummonerByNameAndRegionAsync(String summonerName, String selectedRegion)
        {
            var uri = String.Concat("https://", selectedRegion, SummonerByNameEndpoint, summonerName, "?api_key=", _retrieveApiKeyService.RetrieveApiKey());
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SummonerDTO>(content);
        }
    }
}
