using Business.Dtos;
using Business.Models;
using Data.Entities;
using Data.Models;
using Data.Repositories;
using Domain.Extentions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Business.Services;

public interface IProjectService
{
    Task<ProjectResult> CreateProjectAsync(AddProjectFormData formData);
    Task<ProjectResult<Project>> GetProjectAsync(string id);
    Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync();
    Task<ProjectResult> UpdateProjectAsync(EditProjectFormData formData);
    Task<ProjectResult> DeleteProjectAsync(string projectId);
}

public class ProjectService(IProjectRepository projectRepository, IClientRepository clientRepository, IStatusRepository statusRepository, IClientService clientService) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IStatusRepository _statusRepository = statusRepository;
    private readonly IClientService _clientService = clientService;

    public async Task<ProjectResult> CreateProjectAsync(AddProjectFormData formData)
    {
        var status = await _statusRepository.GetAsync(x => x.StatusName.ToLower() == formData.Status.Trim().ToLower());

        var clientResult = await _clientService.CreateClientAsync(formData.ClientName);
        if (!clientResult.Succeeded)
        {
            return new ProjectResult { Succeeded = false, Error = $"Client error: {clientResult.Error}" };
        }

        if (formData == null)
        {
            return new ProjectResult { Succeeded = false, StatusCode = 400, Error = "Not all required fields are filled" };
        }

        var projectEntity = new ProjectEntity
        {
            Id = Guid.NewGuid().ToString(),
            ProjectName = formData.ProjectName,
            Description = formData.Description,
            StartDate = formData.StartDate,
            EndDate = formData.EndDate,
            Budget = formData.Budget,
            ClientId = clientResult.Result!.Id,
            StatusId = status!.Id
        };

        var result = await _projectRepository.AddAsync(projectEntity);
        return result.Succeeded
            ? new ProjectResult { Succeeded = true, StatusCode = 201 }
            : new ProjectResult { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };

    }

    public async Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync()
    {
        var response = await _projectRepository.GetProjectsAsync();
        return new ProjectResult<IEnumerable<Project>> { Succeeded = true, Result = response.Result };
    }

    public async Task<ProjectResult<Project>> GetProjectAsync(string id)
    {
        var response = await _projectRepository.GetAsync(where: x => x.Id == id, i => i.Status, i => i.Client);
        return response.MapTo<ProjectResult<Project>>();
    }

    public async Task<ProjectResult> UpdateProjectAsync(EditProjectFormData formData)
    {

        if (formData == null)
        {
            return new ProjectResult { Succeeded = false, Error = "Form data is required" };
        }

        var project = await _projectRepository.GetAsync(
            p => p.Id == formData.ProjectId,
            include: q => q.Include(p => p.Client)
                           .Include(p => p.Status));

        if (project == null)
        {
            return new ProjectResult { Succeeded = false, Error = "Project not found" };
        }

        project.ProjectName = formData.ProjectName;
        project.Description = formData.Description;
        project.StartDate = formData.StartDate;
        project.EndDate = formData.EndDate;
        project.Budget = formData.Budget;

        var client = await _clientRepository.GetAsync(x => x.ClientName == formData.ClientName);
        if (client == null)
        {
            client = await _clientRepository.AddAsync(new ClientEntity
            {
                ClientName = formData.ClientName,
            });
        }
        project.ClientId = client.Id;


        var status = await _statusRepository.GetAsync(x => x.StatusName == formData.Status);
        if (status == null)
        {
            return new ProjectResult { Succeeded = false, Error = "Invalid status" };
        }
        project.StatusId = status.Id;

        var updateResult = await _projectRepository.UpdateAsync(project);
        return new ProjectResult { Succeeded = updateResult.Succeeded, Error = updateResult.Error };
    }

    public async Task<ProjectResult> DeleteProjectAsync(string projectId)
    {
        if (string.IsNullOrEmpty(projectId))
        {
            return new ProjectResult { Succeeded = false, Error = "Project Id is empty." };
        }

        var project = await _projectRepository.GetAsync(x => x.Id == projectId);
        if (project == null)
        {
            return new ProjectResult { Succeeded = false, Error = "No project found." };
        }

        var result = await _projectRepository.DeleteAsync(project);
        return new ProjectResult { Succeeded = result.Succeeded, Error = result.Error };
    }
}
