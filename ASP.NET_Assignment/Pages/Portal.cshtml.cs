using ASP.NET_Assignment.Pages.Shared.Partials.Sections;
using Business.Models;
using Business.Services;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;


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
    public EditProjectFormData EditProjectFormData { get; set; } = new EditProjectFormData();

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
            Projects = result.Result!.ToList();
            EditAndDeleteModel.ProjectId = Projects.FirstOrDefault()!.Id;
        }
        else
        {
            Projects = new List<Project>();
        }
        StatusCount();
    }


    public async Task<IActionResult> OnPostAddProject()
    {
        var formKeys = Request.Form.Keys;
        ModelState.Remove("ProjectId");
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                );

            foreach (var error in errors)
            {
                Debug.WriteLine($"Field: {error.Key}, Errors: {string.Join(", ", error.Value!)}");
            }

            return BadRequest(new { success = false, errors = errors });
        }
    
        var result = await _projectService.CreateProjectAsync(AddProjectFormData);
        if (!result.Succeeded)
        {
            return BadRequest(new { success = false, error = result.Error });
        }

        var projectsResult = await _projectService.GetProjectsAsync();
        return RedirectToPage("/Portal");
    }

    public async Task<IActionResult> OnGetEditProject(string projectId)
    {
        var projectResult = await _projectService.GetProjectAsync(projectId);
        if (projectResult.Succeeded && projectResult.Result != null)
        {
            var project = projectResult.Result;
            EditProjectFormData = new EditProjectFormData
            {
                ProjectId = projectId,
                ProjectName = project.ProjectName,
                ClientName = project.Client?.ClientName ?? "Unknown Client",
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status?.StatusName ?? "Not yet started",
                Budget = project.Budget
            };
        }
        return Page();
    }

    public async Task<IActionResult> OnPostEditProject()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _projectService.UpdateProjectAsync(EditProjectFormData);
        if (!result.Succeeded)
        {
            return BadRequest();
        }

        return RedirectToPage("/Portal");
    }


    public async Task<IActionResult> OnPostDeleteProject(string projectId)
    {
        if (string.IsNullOrEmpty(projectId))
        {
            return BadRequest();
        }

        var result = await _projectService.DeleteProjectAsync(projectId);
        if (!result.Succeeded)
        {
            return BadRequest();
        }

        return RedirectToPage("/Portal");
    }
}
