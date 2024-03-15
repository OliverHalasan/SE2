using ClubBAIST.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ClubBAIST
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuration
            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

            // Authentication setup
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(cookieOptions =>
                {
                    cookieOptions.LoginPath = "/Login";
                    cookieOptions.LogoutPath = "/Admin/Logout";
                    cookieOptions.SlidingExpiration = true;
                    cookieOptions.AccessDeniedPath = "/Admin/Forbidden";
                    cookieOptions.ExpireTimeSpan = TimeSpan.FromDays(2); // Make this smaller for production
                    cookieOptions.Cookie.HttpOnly = true;
                });

            // Authorization setup
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("MemberOnly", policy =>
                    policy.RequireRole("Member"));

                options.AddPolicy("StaffOnly", policy =>
                    policy.RequireRole("Staff"));
            });

            // Razor Pages
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<CLB>();


            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapRazorPages();

            app.Run();
        }
    }
}
