using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Wba.StovePalace.Helpers;
using Wba.StovePalace.Models;

namespace Wba.StovePalace.Pages.Users
{
    public class LoginModel : PageModel
    {
        private readonly Wba.StovePalace.Data.StoveContext _context;

        public LoginModel(Wba.StovePalace.Data.StoveContext context)
        {
            _context = context;
        }

        public string Email { get; set; }
        public string Password { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPost(string email, string password)
        {
            Email = email;
            Password = Encoding.EncodePassword(password);
            User user = _context.User.FirstOrDefault(u => u.Email == Email && u.Password == Password);
            if (user == null)
            {
                return RedirectToPage("./Login");
            }
            else
            {
                //CookieOptions cookieOptions = new CookieOptions();
                //cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(365));
                //HttpContext.Response.Cookies.Append("UserId", user.Id.ToString(), cookieOptions);
                return RedirectToPage("../Stoves/Index");
            }
        }
    }
}
