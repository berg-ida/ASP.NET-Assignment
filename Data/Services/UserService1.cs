//using Business.Models;
//using Data.Entities;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//namespace Data.Services;

//public class UserService1(UserManager<UserEntity> userManager)
//{
//    private readonly UserManager<UserEntity> _userManager = userManager;

//    public async Task<bool> ExistsAsync(string email)
//    {
//        if (await _userManager.Users.AnyAsync(x => x.Email == email))
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }

//    public async Task<bool> CreateAsync(UserSignUpForm form)
//    {
//        if (form != null)
//        {
//            var userEntity = new UserEntity
//            {
//                UserName = form.Email,
//                FullName = form.FullName,
//                Email = form.Email,
//            };

//            var result = await _userManager.CreateAsync(userEntity, form.Password);

//            if (!result.Succeeded)
//            {
//                foreach (var error in result.Errors)
//                {
//                    Console.WriteLine($"Error: {error.Description}");
//                }
//                return false;
//            }

//            return result.Succeeded;
//        }
//        return false;
//    }
//}
