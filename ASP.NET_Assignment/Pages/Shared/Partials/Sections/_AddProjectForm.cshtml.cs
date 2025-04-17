using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ASP.NET_Assignment.Pages.Shared.Partials.Sections;

public class _AddProjectFormModel : PageModel
{
    [BindProperty]
    public AddProjectFormData Form { get; set; }

    public void OnGet()
    {
    }

}
