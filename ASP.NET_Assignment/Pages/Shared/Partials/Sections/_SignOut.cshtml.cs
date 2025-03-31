using Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NET_Assignment.Pages.Shared.Partials.Sections
{
    public class _SignOutModel(SignInManager<AppUser> signInManager) : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager = signInManager;

        public void OnGet()
        {
        }

        public async Task <IActionResult> OnPost()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Login");
        }
    }
}
