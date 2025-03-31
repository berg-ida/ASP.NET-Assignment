using Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data.Services;

public class UserService(UserManager<AppUser> userManager)
{
    private readonly UserManager<AppUser> _userManager = userManager;

    public async Task<bool> ExistsAsync(string email)
    {
        if (await _userManager.Users.AnyAsync(x => x.Email == email))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> CreateAsync(UserSignUpForm form)
    {
        if (form != null)
        {
            var appUser = new AppUser
            {
                UserName = form.Email,
                FullName = form.FullName,
                Email = form.Email,
            };

            var result = await _userManager.CreateAsync(appUser, form.Password);

            if (!result.Succeeded)
            {
                // Log or breakpoint here to inspect errors
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error: {error.Description}");
                }
                return false;
            }

            return result.Succeeded;
        }
        return false;
    }
}
