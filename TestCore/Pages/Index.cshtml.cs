using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
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

        private string RetrievedRegion { get; set; }

        public IEnumerable<SelectListItem> Items { get; set; }
            = new List<SelectListItem>
            {
            new SelectListItem{Value= "csharp", Text="C#"},
            new SelectListItem{Value= "python", Text= "Python"},
            new SelectListItem{Value= "cpp", Text="C++"},
            new SelectListItem{Value= "java", Text="Java"},
            new SelectListItem{Value= "js", Text="JavaScript"},
            new SelectListItem{Value= "ruby", Text="Ruby"},
            };
        [BindProperty]
        public InputModel Input { get; set; }
        public Index(ISearchSummonerService searchSummoner, IMatchHistoryService matchHistory, IRetrieveRegionService retrieveRegion)
        {
            _searchSummonerService = searchSummoner;
            _matchHistoryService = matchHistory;
            _retrieveRegionService = retrieveRegion;
        }

        public void OnGet() { }

        public void OnPost() { }

        public async Task<IActionResult> OnPostSearchSummonerAsync(InputModel Input)
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
        public string SelectedValue1 { get; set; } 
    }
}

