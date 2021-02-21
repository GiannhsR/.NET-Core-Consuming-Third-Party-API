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
        private const string SummonerByName = "/lol/summoner/v4/summoners/by-name/";

        private readonly IConfiguration _config;

        private readonly IHttpClientFactory _httpClientFactory;
        public ISearchSummonerService(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        public string RetrieveApiKey()
        {
            return _config.GetValue<string>("API_KEY");
        }

        public async Task<SummonerDTO> SearchSummonerByNameAndRegionAsync(String searchTerm,String selectedRegion)
        {
            var uri = String.Concat("https://",RetrieveRegion(selectedRegion),SummonerByName,searchTerm,"?api_key=",RetrieveApiKey());
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SummonerDTO>(content);
        }

        public string RetrieveRegion(string selectedRegion)
        {
            switch (selectedRegion)
            {
                case "EUNE":
                    return "eun1.api.riotgames.com";
                case "EUW":
                    return "euw1.api.riotgames.com";
                case "NA":
                    return "na1.api.riotgames.com";
                default:
                    return "eun1.api.riotgames.com";
            }
        }
    }
}
