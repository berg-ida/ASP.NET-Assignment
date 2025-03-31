using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class AddProjectForm
{
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Please enter a project name.")]
    public string ProjectName { get; set; } = null!;

    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Please enter a client name.")]
    public string ClientName { get; set; } = null!;

    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Please enter a description.")]
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
    [Required(ErrorMessage = "Please enter a budget.")]
    public string Budget { get; set; } = null!;

}
