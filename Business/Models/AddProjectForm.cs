using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class AddProjectForm
{
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
    public string StartDate { get; set; } = null!;

    [DataType(DataType.Date)]
    public string EndDate { get; set; } = null!;

    [DataType(DataType.Text)]
    public string Status { get; set; } = null!;
    public List<string> StatusOptions { get; } = new List<string>
    {
        "Not yet started",
        "Started",
        "Completed"
    };


    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    public string Budget { get; set; } = null!;

}
