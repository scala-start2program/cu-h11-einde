using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Wba.StovePalace.Models;
using Wba.StovePalace.Helpers;

namespace Wba.StovePalace.Pages.Users
{
    public class RegisterModel : PageModel
    {
        private readonly Wba.StovePalace.Data.StoveContext _context;

        public RegisterModel(Wba.StovePalace.Data.StoveContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            User.Password = Encoding.EncodePassword(User.Password);
            _context.User.Add(User);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return Page();
            }

            //// deze code hebben we later nodig
            //CookieOptions cookieOptions = new CookieOptions();
            //cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(365));
            //HttpContext.Response.Cookies.Append("UserId", User.Id.ToString(), cookieOptions);

            return RedirectToPage("../Stoves/Index");
        }
    }
}
