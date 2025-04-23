using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NET_Assignment.Pages;

public class RegisterModel(IUserService userService) : PageModel
{
    private readonly IUserService _userService = userService;

    [BindProperty]
    public SignUpFormData Form { get; set; } = new SignUpFormData();

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

        var result = await _userService.CreateUserAsync(Form);
        if (result.Succeeded)
        {
            return RedirectToPage("/Login");
        }

        ModelState.AddModelError("NotCreated", "User was not created.");
        return Page();
    }

}

