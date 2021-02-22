using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestCore.Services;
using TestCore.HelperClasses;
using System.ComponentModel.DataAnnotations;
using TestCore.DTO;

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

        private string RetrievedRegion { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        public string API_KEY { get; set; }
        public Index(ISearchSummonerService searchSummoner, IMatchHistoryService matchHistory, IRetrieveRegionService retrieveRegion)
        {
            _searchSummonerService = searchSummoner;
            _matchHistoryService = matchHistory;
            _retrieveRegionService = retrieveRegion;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(InputModel Input)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            RetrievedRegion = _retrieveRegionService.RetrieveRegion(Input.SelectedRegion);
            _summonerDTO = await _searchSummonerService.SearchSummonerByNameAndRegionAsync(Input.SearchTerm, RetrievedRegion);
            _matchHistoryDTO = await _matchHistoryService.MatchHistoryByAccountIdAndRegionAsync(_summonerDTO.AccountId, RetrievedRegion);
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
    }
}

