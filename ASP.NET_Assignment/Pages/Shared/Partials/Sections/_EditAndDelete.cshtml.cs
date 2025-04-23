using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NET_Assignment.Pages.Shared.Partials.Sections;

public class _EditAndDeleteModel : PageModel
{

    [BindProperty]
    public EditProjectFormData EditProjectFormData { get; set; } = new();
    public string ProjectId { get; set; } = null!;

    public void OnGet()
    {

    }
}
