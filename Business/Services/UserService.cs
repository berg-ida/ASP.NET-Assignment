using Business.Dtos;
using Business.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.Extentions;
using Azure.Core;

namespace Business.Services;

public interface IUserService
{
    Task<UserResult> CreateUserAsync(SignUpFormData formData);
    Task<bool> ExistsAsync(string email);
    Task<UserResult> GetUsersAsync();
}

public class UserService(IUserRepository userRepository, UserManager<UserEntity> userManager) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly UserManager<UserEntity> _userManager = userManager;

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

    public async Task<UserResult> CreateUserAsync(SignUpFormData formData)
    {
        if (formData == null)
        {
            return new UserResult { Succeeded = false, StatusCode = 400, Error = "Form data cannot be null" };
        }

        var exists = await _userRepository.ExistsAsync(x => x.Email == formData.Email);
        if (exists.Succeeded)
        {
            return new UserResult { Succeeded = false, StatusCode = 409, Error = "User with the same email already exists." };
        }

        try
        {
            var userEntity = new UserEntity
            {
                UserName = formData.Email,
                FullName = formData.FullName,
                Email = formData.Email,
            };
            var result = await _userManager.CreateAsync(userEntity, formData.Password);
            return new UserResult { Succeeded = result.Succeeded, StatusCode = result.Succeeded ? 200 : 400, Error = result.Succeeded ? null : string.Join(",", result.Errors.Select(e => e.Description)) };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new UserResult { Succeeded = false, StatusCode = 500, Error = ex.Message };
        }

    }

    public async Task<UserResult> GetUsersAsync()
    {
        var result = await _userRepository.GetAllAsync();
        return result.MapTo<UserResult>();
    }
}
