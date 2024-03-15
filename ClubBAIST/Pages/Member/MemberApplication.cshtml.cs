using ClubBAIST.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ClubBAIST.Pages.Member
{
    public class MemberApplicationModel : PageModel
    {

        [BindProperty]
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Postal Code is required.")]
        [RegularExpression(@"^((\d{5}-\d{4})|(\d{5})|([AaBbCcEeGgHhJjKkLlMmNnPpRrSsTtVvXxYy]\d[A-Za-z]\s?\d[A-Za-z]\d))$", ErrorMessage = "Invalid Postal Code")]
        public string PostalCode { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public string Phone { get; set; }



        [BindProperty]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public string? AlterPhone { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Company name is required.")]
        public string CompanyName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Address Line 1 is required.")]
        public string AddressLine1 { get; set; }

        [BindProperty]
        public string? AddressLine2 { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date.")]
        public DateTime DateOfBirth { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Membership Type ID is required.")]
        public int MembershipType { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        public string Role { get; set; }

        public bool Confirmation { get; set; }

        public string Message { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        public string Occupation { get; set; } = string.Empty;



        public void OnGet()
        {
            // Optional: Logic for handling GET requests
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                CLB RequestDirector = new();
                Confirmation = RequestDirector.NewMemberApplication(FirstName, LastName, Address, PostalCode, Phone, AlterPhone, CompanyName, AddressLine1, AddressLine2, Email, DateOfBirth, MembershipType, Password, Role, Occupation);
                
                    if (Confirmation)
                {
                    Message = "Success";
                }
                    else
                {
                    Message = "FAIL";
                }
            }
        }
    }
}