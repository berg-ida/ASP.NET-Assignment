using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class UserSignInForm
{
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Required")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Required")]
    public string Password { get; set; } = null!;

    public bool RememberMe { get; set; }
}
