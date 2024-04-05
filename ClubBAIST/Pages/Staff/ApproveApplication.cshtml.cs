using ClubBAIST.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBAIST.Pages.Staff
{
    public class ApproveApplicationModel : PageModel
    {

        [BindProperty]
        public List<Model.Member> ApplicationList { get; set; } = new List<Model.Member>();

        public void OnGet()
        {
            CLB RequestDirector = new();
            List<Model.Member> newMembers = RequestDirector.ApprovedMemberApplication();

            foreach (Model.Member app in newMembers)
            {
                ApplicationList.Add(new Model.Member
                {
                    MemberID = app.MemberID,
                    FirstName = app.FirstName,
                    LastName = app.LastName,
                    Address = app.Address,
                    PostalCode = app.PostalCode,
                    Phone = app.Phone,
                    AlterPhone = app.AlterPhone,
                    CompanyName = app.CompanyName,
                    AddressLine1 = app.AddressLine1,
                    AddressLine2 = app.AddressLine2,
                    Email = app.Email,
                    DateOfBirth = app.DateOfBirth,
                    MembershipTypeID = app.MembershipTypeID,
                    Role = app.Role,
                    Approved = app.Approved,
                });
            }
        }
    }
}
