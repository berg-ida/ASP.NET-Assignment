using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NET_Assignment.Pages.Shared.Partials.Sections
{
    public class _AddProjectFormModel
    {
        [BindProperty]
        public AddProjectForm Form { get; set; }
        public void OnGet()
        {
        }

        public void OnPost()
        {
            
        }
    }
}
