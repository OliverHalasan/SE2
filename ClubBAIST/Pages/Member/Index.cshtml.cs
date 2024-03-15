using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBAIST.Pages.Member
{
    [Authorize(Policy = "MemberOnly")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
