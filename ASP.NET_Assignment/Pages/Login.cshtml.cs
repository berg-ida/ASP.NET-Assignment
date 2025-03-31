using Business.Models;
using Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NET_Assignment.Pages
{
    public class LoginModel(UserService userService, SignInManager<AppUser> signInManager) : PageModel
    {
        private readonly UserService _userService = userService;
        private readonly SignInManager<AppUser> _signInManager = signInManager;

        [BindProperty]
        public UserSignInForm Form { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Form.Email, Form.Password, Form.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToPage("/Portal");
                }
            }
            return Page();

        }

    }
}
