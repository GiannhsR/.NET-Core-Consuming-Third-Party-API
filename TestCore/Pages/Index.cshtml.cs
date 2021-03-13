using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TestCore.DAL;
using TestCore.DAL.Models;
using TestCore.DAL.RepositoryServices;
using TestCore.DTO;
using TestCore.Filters;
using TestCore.Services;

namespace TestCore.Pages
{
    [LogResourceFilter]
    [FeatureEnabled(IsEnabled = true)]
    public class Index : PageModel
    {
        //Services
        private readonly ISearchSummonerService _searchSummonerService;
        private readonly IMatchHistoryService _matchHistoryService;
        private readonly IRetrieveRegionService _retrieveRegionService;
        private readonly IMyDatabaseService _myDatabaseService;

        //DTOs
        public SummonerDTO _summonerDTO;
        public MatchHistoryDTO _matchHistoryDTO;
        public List<ChampionDTO> _champions;

        private string RetrievedRegion { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        public Index(ISearchSummonerService searchSummoner,
            IMatchHistoryService matchHistory,
            IRetrieveRegionService retrieveRegion,
            IMyDatabaseService myDatabaseService)

        {
            _searchSummonerService = searchSummoner;
            _matchHistoryService = matchHistory;
            _retrieveRegionService = retrieveRegion;
            _myDatabaseService = myDatabaseService;;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await _myDatabaseService.GetSearchesAsync();
            await _myDatabaseService.GetSingleSearchAsync(1);
            return Page();
        }

        public async Task<IActionResult> OnPostSearchSummonerAsync(InputModel Input)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            RetrievedRegion = _retrieveRegionService.RetrieveRegion(Input.SelectedRegion);
            _summonerDTO = await _searchSummonerService.SearchSummonerByNameAndRegionAsync(Input.SearchTerm, RetrievedRegion);
            await _myDatabaseService.StoreSearchQuery(_summonerDTO);
            _matchHistoryDTO = await _matchHistoryService.MatchHistoryByAccountIdAndRegionAsync(_summonerDTO.AccountId, RetrievedRegion, endIndex: 10);
            _champions = await _matchHistoryService.MapMatchHistoryAPIDataToDataDragonAsync(_matchHistoryDTO);
            return Page();
        }

        public async Task<IActionResult> OnGetShowMoreAsync(InputModel Input)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            RetrievedRegion = _retrieveRegionService.RetrieveRegion(Input.SelectedRegion);
            _summonerDTO = await _searchSummonerService.SearchSummonerByNameAndRegionAsync(Input.SearchTerm, RetrievedRegion);
            _matchHistoryDTO = await _matchHistoryService.MatchHistoryByAccountIdAndRegionAsync(_summonerDTO.AccountId, RetrievedRegion, Input.EndIndex);
            _champions = await _matchHistoryService.MapMatchHistoryAPIDataToDataDragonAsync(_matchHistoryDTO);
            return Page();
        }
    }

    public class InputModel
    {
        [Required(ErrorMessage = "Required")]
        [MaxLength(64)]
        public string SearchTerm { get; set; }
        [Required(ErrorMessage = "Required")]
        public string SelectedRegion { get; set; }
        public int EndIndex { get; set; }
    }
}

