using ASP.NET_Assignment.Pages.Shared.Partials.Sections;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NET_Assignment.Pages;


[Authorize]
public class PortalModel(_SignOutModel signOutModel) : PageModel
{
    [BindProperty]
    public AddProjectForm AddProjectForm { get; set; } = new AddProjectForm();

    public _SignOutModel SignOutModel = signOutModel;

    public void OnGet()
    {
    }
}
