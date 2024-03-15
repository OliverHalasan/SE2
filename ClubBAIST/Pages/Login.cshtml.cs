using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using ClubBAIST.Model;
using Microsoft.AspNetCore.Identity;

namespace ClubBAIST.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string Message { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public string? RedirectUri { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                CLB RequestDrector = new CLB(); // Create an instance of your CLB class
                Model.Member existingMember = RequestDrector.Login(Email, Password);

                if (existingMember != null)
                {
                    string UserEmail = existingMember.Email;
                    string UserName = existingMember.FirstName + " " + existingMember.LastName;
                    string UserPassword = existingMember.PasswordHash;
                    string UserRole = existingMember.Role; // You may fetch user role from the database
                    int UserMemberID = existingMember.MemberID;

                    if (Email == UserEmail && Password == UserPassword)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, Email),
                            new Claim(ClaimTypes.Name, UserName),
                            new Claim(ClaimTypes.Role, UserRole),
                            new Claim("MemberID", UserMemberID.ToString()) // Custom claim type for MemberID

                        };
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, UserRole));

                        AuthenticationProperties authProperties = new AuthenticationProperties
                        {
                            // Configure additional authentication properties if needed
                        };

                        var returnUrl = Request.Cookies["ReturnUrl"];
                        Response.Cookies.Delete("ReturnUrl");

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                          new ClaimsPrincipal(claimsIdentity), authProperties);

                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        // If no return URL, redirect to a default page
                        if (UserRole == "Member")
                        {
                            return RedirectToPage("/Member/Index");
                        }
                    }
                    else
                    {
                        Message = "Invalid email or password";
                    }
                }
                else
                {
                    Message = "Invalid email or password";
                }
            }
            else
            {
                Message = "Invalid email or password";
            }

            // If the ModelState is not valid, return to the login page with error message
            return Page();
        }
    }
}
