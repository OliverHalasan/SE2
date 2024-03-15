using ClubBAIST.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBAIST.Pages.TeeTimePage
{
    [Authorize]
    public class BookTeeTimeModel : PageModel
    {

        [BindProperty]
        public string MemberNumber1 { get; set; }

        [BindProperty]
        public string? MemberNumber2 { get; set; }
        [BindProperty]
        public string? MemberNumber3 { get; set; }
        [BindProperty]
        public string? MemberNumber4 { get; set; }
        [BindProperty]
        public string Name1 { get; set; }
        [BindProperty]
        public string? Name2 { get; set; }
        [BindProperty]
        public string? Name3 { get; set; }
        [BindProperty]
        public string? Name4 { get; set; }
        [BindProperty]
        public TimeOnly RequestedTeeTime { get; set; }
        [BindProperty]
        public DateOnly RequestedDate { get; set; }

        public bool Confirmation { get; set; }

        public string Message { get; set; } = string.Empty;



        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                // Validate the time to be between 7 AM and 5 PM
                if (RequestedTeeTime < new TimeOnly(07, 00, 00) || RequestedTeeTime > new TimeOnly(17, 00, 00))
                {
                    ModelState.AddModelError(nameof(RequestedTeeTime), "Tee time must be between 7 AM and 5 PM.");
                }

                // Validate the date to be within the current week (Sunday to Saturday) or the following week
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                int daysUntilSunday = ((int)DayOfWeek.Sunday - (int)today.DayOfWeek + 7) % 7;
                DateOnly nextSunday = today.AddDays(daysUntilSunday);
                DateOnly followingSunday = nextSunday.AddDays(7);

                if (!(today <= RequestedDate && RequestedDate <= nextSunday) &&
                    !(followingSunday <= RequestedDate && RequestedDate <= followingSunday.AddDays(6)))
                {
                    ModelState.AddModelError(nameof(RequestedDate), $"Requested date must be between {today:yyyy-MM-dd} and {followingSunday.AddDays(6):yyyy-MM-dd}.");
                }

                if (ModelState.IsValid)
                {
                    // Proceed with booking
                    CLB RequestDirection = new();
                    Confirmation = RequestDirection.newTeeTime(MemberNumber1, Name1, MemberNumber2, Name2, MemberNumber3, Name3, MemberNumber4, Name4, RequestedTeeTime, RequestedDate);

                    if (Confirmation)
                    {
                        Message = "Success";
                    }
                    else
                    {
                        Message = "Fail";
                    }
                }
            }
        }



    }
}
