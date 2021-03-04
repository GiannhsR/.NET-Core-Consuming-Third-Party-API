using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TestCore.DAL.Models;
using TestCore.DAL.RepositoryServices;
using TestCore.DTO;
using TestCore.Services;

namespace TestCore.Pages
{
    public class Index : PageModel
    {
        //Services
        private readonly ISearchSummonerService _searchSummonerService;
        private readonly IMatchHistoryService _matchHistoryService;
        public readonly IRetrieveRegionService _retrieveRegionService;

        //DTOs
        public SummonerDTO _summonerDTO;
        public MatchHistoryDTO _matchHistoryDTO;
        public List<ChampionDTO> _champions;

        private string RetrievedRegion { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        public Index(ISearchSummonerService searchSummoner, IMatchHistoryService matchHistory, IRetrieveRegionService retrieveRegion)
        {
            _searchSummonerService = searchSummoner;
            _matchHistoryService = matchHistory;
            _retrieveRegionService = retrieveRegion;
        }

        public async Task<IActionResult> OnPostSearchSummonerAsync(InputModel Input)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            RetrievedRegion = _retrieveRegionService.RetrieveRegion(Input.SelectedRegion);
            _summonerDTO = await _searchSummonerService.SearchSummonerByNameAndRegionAsync(Input.SearchTerm, RetrievedRegion);
            _matchHistoryDTO = await _matchHistoryService.MatchHistoryByAccountIdAndRegionAsync(_summonerDTO.AccountId, RetrievedRegion, endIndex: 10);
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
        public int EndIndex { get; set; } = 10;
    }
}

