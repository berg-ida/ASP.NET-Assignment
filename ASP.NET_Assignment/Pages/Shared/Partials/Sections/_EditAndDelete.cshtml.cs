using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NET_Assignment.Pages.Shared.Partials.Sections
{
    public class _EditAndDeleteModel : PageModel
    {
        [BindProperty]
        public EditProjectForm EditProjectForm { get; set; } = new EditProjectForm();

        public void OnGet()
        {
        }
    }
}
