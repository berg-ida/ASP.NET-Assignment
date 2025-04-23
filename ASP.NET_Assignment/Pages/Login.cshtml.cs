using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NET_Assignment.Pages;

public class LoginModel(SignInManager<UserEntity> signInManager) : PageModel
{
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    [BindProperty]
    public SignInFormData Form { get; set; } = new SignInFormData();    


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
            else
            {
                Console.WriteLine("Login failed");
            }
        }
        return Page();
    }
}
