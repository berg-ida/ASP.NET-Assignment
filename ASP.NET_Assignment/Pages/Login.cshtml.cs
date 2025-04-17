using Business.Models;
using Business.Services;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NET_Assignment.Pages;

public class LoginModel(IUserService userService, SignInManager<UserEntity> signInManager) : PageModel
{
    private readonly IUserService _userService = userService;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    [BindProperty]
    public SignInFormData Form { get; set; }


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
