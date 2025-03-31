using Business.Models;
using Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NET_Assignment.Pages
{
    public class RegisterModel(UserService userService) : PageModel
    {
        private readonly UserService _userService = userService;

        [BindProperty]
        public UserSignUpForm Form { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost() 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (await _userService.ExistsAsync(Form.Email))
            {
                ModelState.AddModelError("Exists", "User already exists.");
                return Page();
            }

            var result = await _userService.CreateAsync(Form);
            if (result)
            {
                return RedirectToPage("/Login");
            }

            ModelState.AddModelError("NotCreated", "User was not created.");
            return Page();
        }
    }
}
