using ClubBAIST.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBAIST.Pages.TeeTimePage
{
    public class UpdateTeeTimeModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int StandingID { get; set; }
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
        public TimeSpan RequestedTeeTime { get; set; }
        [BindProperty]
        public DateTime RequestedDate { get; set; }

        public bool Confirmation { get; set; }

        public string Message { get; set; } = string.Empty;

        public void OnGet()
        {
            // Retrieve the ID from the URL parameter "id"
            if (Request.Query.TryGetValue("id", out var id))
            {
                CLB RequstDirector = new();
                TeeTimeLists ActiveTeeTime = new();
                if (int.TryParse(id, out var standingID))
                {
                    StandingID = standingID;
                    
                }
                ActiveTeeTime = RequstDirector.GetStandingTeeTime(standingID);
                MemberNumber1 = ActiveTeeTime.MemberNumber1;
                Name1 = ActiveTeeTime.name1;
                MemberNumber2 = ActiveTeeTime?.MemberNumber2;
                Name2 = ActiveTeeTime.name2;
                MemberNumber3 = ActiveTeeTime?.MemberNumber3;
                Name3 = ActiveTeeTime.name3;
                MemberNumber4 = ActiveTeeTime?.MemberNumber4;
                Name4 = ActiveTeeTime.name4;
                RequestedDate = ActiveTeeTime.RequestedDate;
                RequestedTeeTime = ActiveTeeTime.RequestedTeeTime;



            }
        }

        public void OnPost()
        {

            if (ModelState.IsValid)
            {
                // Validate the time to be between 7 AM and 5 PM
                if (RequestedTeeTime < new TimeSpan(07, 00, 00) || RequestedTeeTime > new TimeSpan(17, 00, 00))
                {
                    ModelState.AddModelError(nameof(RequestedTeeTime), "Tee time must be between 7 AM and 5 PM.");
                }

               

                if (ModelState.IsValid)
                {
                    CLB RequestDirector = new();
                    StandingTeeTime BookedTeeTime = new();
                    DateOnly requestedDate = new DateOnly(RequestedDate.Year, RequestedDate.Month, RequestedDate.Day);

                    // Convert RequestedTeeTime to TimeOnly
                    TimeOnly requestedTeeTime = new TimeOnly(RequestedTeeTime.Hours, RequestedTeeTime.Minutes, RequestedTeeTime.Seconds);

                    BookedTeeTime.StandingID = StandingID;
                    BookedTeeTime.MemberNumber1 = MemberNumber1;
                    BookedTeeTime.name1 = Name1;
                    BookedTeeTime.MemberNumber2 = MemberNumber2;
                    BookedTeeTime.name2 = Name2;
                    BookedTeeTime.MemberNumber3 = Name3;
                    BookedTeeTime.name3 = Name3;
                    BookedTeeTime.MemberNumber4 = Name4;
                    BookedTeeTime.name4 = Name4;
                    BookedTeeTime.RequestedDate = requestedDate;
                    BookedTeeTime.RequestedTeeTime = requestedTeeTime;

                    Confirmation = RequestDirector.ModifyTeeTime(StandingID, MemberNumber1, Name1, MemberNumber2, Name2, MemberNumber3, Name3, MemberNumber4, Name4, requestedTeeTime, requestedDate);

                    if (Confirmation)
                    {
                        Message = "Modify Sucess";
                        RedirectToPage("/TeeTimePage/TeeTimeList");
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
