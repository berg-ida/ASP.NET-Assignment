using ASP.NET_Assignment.Pages.Shared.Partials.Sections;
using Business.Models;
using Business.Services;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ASP.NET_Assignment.Pages;

[Authorize]
public class PortalModel(IProjectService projectService, _SignOutModel signOutModel, _EditAndDeleteModel editAndDeleteModel) : PageModel
{
    private readonly IProjectService _projectService = projectService;
    public _SignOutModel SignOutModel = signOutModel;
    public _EditAndDeleteModel EditAndDeleteModel = editAndDeleteModel;

    [BindProperty]
    public AddProjectFormData AddProjectFormData { get; set; } = new AddProjectFormData();

    [BindProperty]
    public EditProjectForm EditProjectForm { get; set; } = new EditProjectForm();

    public int AllCount {  get; set; }
    public int NotStartedCount { get; set; }
    public int StartedCount { get; set; }
    public int CompletedCount { get; set; }

    public void StatusCount()
    {
        AllCount = Projects.Count();
        NotStartedCount = Projects.Count(x => x.Status?.StatusName == "Not yet started");
        StartedCount = Projects.Count(x => x.Status?.StatusName == "Started");
        CompletedCount = Projects.Count(x => x.Status?.StatusName == "Completed");

    }

    public List<Project> Projects { get; set; } = new List<Project>();
    public async Task OnGetAsync()
    {
        var result = await _projectService.GetProjectsAsync();
        if (result.Succeeded)
        {
            Projects = result.Result.ToList();
        }
        else
        {
            Projects = new List<Project>();
        }
        StatusCount();
    }


    public async Task<IActionResult> OnPostAddProject()
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                );

            return BadRequest(new { success = false, errors });
        }
        var result = await _projectService.CreateProjectAsync(AddProjectFormData);
        if (!result.Succeeded)
        {
            return BadRequest(new { success = false, error = result.Error });
        }

        var projectsResult = await _projectService.GetProjectsAsync();
        return RedirectToPage("/Portal");
    }

}
