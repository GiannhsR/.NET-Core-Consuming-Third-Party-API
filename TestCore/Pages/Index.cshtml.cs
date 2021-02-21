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
        private readonly ISearchSummonerService _searchSummoner;
        private readonly IMatchHistoryService _matchHistory;
        public SummonerDTO _summonerDTO;
        public MatchHistoryDTO _matchHistoryDTO;
        [BindProperty]
        public InputModel Input { get; set; }
        public string API_KEY { get; set; }
        public Index(ISearchSummonerService searchSummoner, IMatchHistoryService matchHistory)
        {
            _searchSummoner = searchSummoner;
            _matchHistory = matchHistory;
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

            _summonerDTO = await _searchSummoner.SearchSummonerByNameAndRegionAsync(Input.SearchTerm, Input.SelectedRegion);
            _matchHistoryDTO = await _matchHistory.MatchHistoryByAccountIdAsync(_summonerDTO.AccountId);
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

