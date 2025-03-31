using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class UserSignInForm
{
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Please enter an email adress.")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Please enter a password.")]
    public string Password { get; set; } = null!;

    public bool RememberMe { get; set; }
}
