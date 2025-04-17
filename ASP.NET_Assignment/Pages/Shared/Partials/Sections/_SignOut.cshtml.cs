using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ASP.NET_Assignment.Pages.Shared.Partials.Sections;

public class _SignOutModel(SignInManager<UserEntity> signInManager) : PageModel
{
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    [BindProperty]
    public string FullName { get; set; } = string.Empty;

    public void OnGet()
    {

    }

    public async Task <IActionResult> OnPost()
    {
        await _signInManager.SignOutAsync();
        return RedirectToPage("/Login");
    }
}
