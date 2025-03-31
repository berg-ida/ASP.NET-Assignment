using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class UserSignUpForm
{
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Please enter both first and last name.")]
    public string FullName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Please enter an email adress.")]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Please enter a valid email adress.")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Please enter a password.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{6,}$", ErrorMessage = "Password needs to be at least 6 characters long.")]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
    [Required(ErrorMessage = "Please confirm password.")]
    public string ConfirmPassword { get; set; } = null!;

    [Required]
    public bool TermsAndConditions { get; set; }
}
