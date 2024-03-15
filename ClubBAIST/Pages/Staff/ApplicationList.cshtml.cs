using ClubBAIST.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ClubBAIST.Pages.Staff
{
    public class ApplicationListModel : PageModel
    {
        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string Address { get; set; }

        [BindProperty]
        public string PostalCode { get; set; }

        [BindProperty]
        public string Phone { get; set; }

        [BindProperty]
        public string AlterPhone { get; set; }

        [BindProperty]
        public string CompanyName { get; set; }

        [BindProperty]
        public string AddressLine1 { get; set; }

        [BindProperty]
        public string AddressLine2 { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public DateTime DateOfBirth { get; set; }

        [BindProperty]
        public int MembershipTypeID { get; set; }

        [BindProperty]
        public string Role { get; set; }

        [BindProperty]
        public List<Model.Member> ApplicationList { get; set; } = new List<Model.Member>();

        public void OnGet()
        {
            CLB RequestDirector = new();
            List<Model.Member> newMembers = RequestDirector.ListApplication();

            foreach (Model.Member app in newMembers)
            {
                ApplicationList.Add(new Model.Member
                {
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
