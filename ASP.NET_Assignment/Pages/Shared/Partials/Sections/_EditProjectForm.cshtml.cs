using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NET_Assignment.Pages.Shared.Partials.Sections;

public class EditProjectFormModel : PageModel
{

    [BindProperty]
    public EditProjectFormData FormData { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string ProjectId { get; set; } = null!;

    public void OnGet()
    {
    }
  
}
