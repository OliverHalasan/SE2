using ClubBAIST.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBAIST.Pages.Staff
{
    public class MembershipApplicationModel : PageModel
    {

        [BindProperty]
        public int MemberID { get; set; }
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
        public DateOnly DateOfBirth { get; set; }

        [BindProperty]
        public int MembershipTypeID { get; set; }

        [BindProperty]
        public string Approved { get; set; }
        public bool Confirmation { get; set; }
        public string Message { get; set; } = string.Empty;


        public void OnGet()
        {
            if (Request.Query.TryGetValue("id", out var id))
            {
                CLB RequestDirector = new();
                Model.Member ExistingMemberApplication = new();

                if (int.TryParse(id, out var memberID))
                {
                    MemberID = memberID;

                }
                ExistingMemberApplication = RequestDirector.FindMembershipApplication(MemberID);
                MemberID = ExistingMemberApplication.MemberID;
                FirstName = ExistingMemberApplication.FirstName;
                LastName = ExistingMemberApplication.LastName;
                Address = ExistingMemberApplication.Address;
                PostalCode = ExistingMemberApplication.PostalCode;
                Phone = ExistingMemberApplication.Phone;
                AlterPhone = ExistingMemberApplication.AlterPhone;
                CompanyName = ExistingMemberApplication.CompanyName;
                AddressLine1 = ExistingMemberApplication.AddressLine1;
                AddressLine2 = ExistingMemberApplication.AddressLine2;
                Email = ExistingMemberApplication.Email;
                DateOfBirth = ExistingMemberApplication.DateOfBirth;
                Approved = ExistingMemberApplication.Approved;

            }


        }

        public void OnPost()
        {
            if (Request.Query.TryGetValue("id", out var id))
            {

                CLB RequestDirector = new();
                Model.Member ApprovedMember = new();

                if (int.TryParse(id, out var memberID))
                {
                    MemberID = memberID;

                }

                ApprovedMember.MemberID = memberID;
                ApprovedMember.Approved = Approved;

                Confirmation = RequestDirector.UpdateMemberApplication(MemberID, Approved);
                if (Confirmation)
                {
                    Message = "Modify Sucess";
                    RedirectToPage("/Staff/ApplicationList");
                }
                else
                {
                    Message = "Fail";
                }
            }
        }

    }
}
