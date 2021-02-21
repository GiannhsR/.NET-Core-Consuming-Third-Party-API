using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestCore.Services;
using TestCore.HelperClasses;
using System.ComponentModel.DataAnnotations;
namespace TestCore.Pages
{
    public class RenderSummonerModel : PageModel
    {
        private readonly ISearchSummonerService _searchSummoner;

        public string _content { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        public string API_KEY { get; set; }
        public RenderSummonerModel(ISearchSummonerService searchSummoner)
        {
            _searchSummoner = searchSummoner;
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

            _content = await _searchSummoner.SearchSummonerByNameAndRegion(Input.SearchTerm, Input.SelectedRegion);
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

