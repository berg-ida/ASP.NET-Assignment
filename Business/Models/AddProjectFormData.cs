using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class AddProjectFormData
{
    public string ProjectId { get; set; } = null!;

    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    public string ProjectName { get; set; } = null!;

    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    public string ClientName { get; set; } = null!;

    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    public string Description { get; set; } = null!;

    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    [DataType(DataType.Text)]
    public string Status { get; set; } = "Not yet started";

    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    public decimal Budget { get; set; }
}
