using ClubBAIST.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ClubBAIST.Pages.TeeTimePage
{
    [Authorize]
    public class TeeTimeListModel : PageModel
    {
        [BindProperty]
        public int MemberID { get; set; }
        public List<TeeTimeLists> TeeTimeList { get; set; } = new List<TeeTimeLists>();

        public void OnGet()
        {
            // Check if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Retrieve the MemberID claim from the user's claims
                var memberIDClaim = User.FindFirst("MemberID");

                // Check if the MemberID claim exists
                if (memberIDClaim != null && int.TryParse(memberIDClaim.Value, out int memberID))
                {
                    // Use the memberID variable to fetch the TeeTimeList
                    CLB RequestDirector = new CLB();
                    TeeTimeList = RequestDirector.FindTeeTime(memberID);
                }
                else
                {
                    // Handle the case where the MemberID claim is missing or invalid
                    // For example, redirect to an error page or display an error message
                }
            }
            else
            {
                // Handle the case where the user is not authenticated
                // For example, redirect to a login page
            }
        }


        public void OnPost()
        {

        }
    }
}
