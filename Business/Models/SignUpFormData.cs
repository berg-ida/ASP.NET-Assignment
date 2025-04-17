using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class SignUpFormData
{
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    public string FullName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Required")]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Please enter a valid email adress.")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Required")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{6,}$", ErrorMessage = "Password needs to be at least 6 characters long.")]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
    [Required(ErrorMessage = "Required")]
    public string ConfirmPassword { get; set; } = null!;

    [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms and conditions.")]
    public bool TermsAndConditions { get; set; }
}
