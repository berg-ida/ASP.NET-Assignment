using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class AppUser : IdentityUser
{
    [ProtectedPersonalData]
    public string FullName { get; set; } = null!;
}

