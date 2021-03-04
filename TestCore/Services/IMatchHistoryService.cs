using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TestCore.DTO;
using Newtonsoft.Json;
using TestCore.DAL.RepositoryServices;
using TestCore.DAL.Models;

namespace TestCore.Services
{
    public class IMatchHistoryService
    {
        private const string MatchHistoryByAccountId = "/lol/match/v4/matchlists/by-account/";
        private readonly IRetrieveApiKeyService _retrieveApiKeyService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IChampionsService _championsService;

        public IMatchHistoryService(IRetrieveApiKeyService retrieveApiKeyService, IHttpClientFactory httpClientFactory, IChampionsService championsService)
        {
            _retrieveApiKeyService = retrieveApiKeyService;
            _httpClientFactory = httpClientFactory;
            _championsService = championsService;
        }

        public async Task<MatchHistoryDTO> MatchHistoryByAccountIdAndRegionAsync(string accountId, string selectedRegion,int endIndex=0,int beginIndex=0)
        {
            if(endIndex != 10)
            {
                beginIndex = endIndex;
            }
            var uri = String.Concat("https://", selectedRegion, MatchHistoryByAccountId, accountId,"?beginIndex=", beginIndex.ToString(), "&endIndex=" ,endIndex.ToString(), "&api_key=", _retrieveApiKeyService.RetrieveApiKey());
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<MatchHistoryDTO>(content);
        }

        public async Task<List<ChampionDTO>> MapMatchHistoryAPIDataToDataDragonAsync(MatchHistoryDTO matchHistoryDTO)
        {
            List<ChampionDTO> championDTOList = new List<ChampionDTO>();
            foreach (var match in matchHistoryDTO.Matches)
            {
                var champion = await _championsService.GetAsync(match.Champion.ToString());
                ChampionDTO championDTO = new ChampionDTO();
                championDTOList.Add(MapChampionToChampionDTO(champion, championDTO));
            }
            return championDTOList;
        }
        private static ChampionDTO MapChampionToChampionDTO(Champion champion, ChampionDTO championDTO)
        {
            championDTO.Id = champion.Id;
            championDTO.ChampionId = champion.ChampionId;
            championDTO.ChampionKey = champion.ChampionKey;
            championDTO.ChampionName = champion.ChampionName;
            championDTO.ChampionTitle = champion.ChampionTitle;
            return championDTO;
        }
    }

}
