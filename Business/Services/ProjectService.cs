using Business.Dtos;
using Business.Models;
using Data.Entities;
using Data.Models;
using Data.Repositories;
using Domain.Extentions;

namespace Business.Services;

public interface IProjectService
{
    Task<ProjectResult> CreateProjectAsync(AddProjectFormData formData);
    Task<ProjectResult<Project>> GetProjectAsync(string id);
    Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync();
}

public class ProjectService(IProjectRepository projectRepository, IStatusService statusService, IClientService clientService) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IStatusService _statusService = statusService;
    private readonly IClientService _clientService = clientService;

    public async Task<ProjectResult> CreateProjectAsync(AddProjectFormData formData)
    {
        var clientResult = await _clientService.CreateClientAsync(formData.ClientName);
        if (!clientResult.Succeeded)
        {
            return new ProjectResult { Succeeded = false, Error = $"Client error: {clientResult.Error}" };
        }

        var statusResult = await _statusService.ExistsAsync(formData.Status);
        if (!statusResult.Succeeded)
        {
            return new ProjectResult { Succeeded = false, Error = $"Status error: {statusResult.Error}" };
        }

        if (formData == null)
        {
            return new ProjectResult { Succeeded = false, StatusCode = 400, Error = "Not all required fields are supplied." };
        }

        var projectEntity = new ProjectEntity
        {
            ProjectName = formData.ProjectName,
            Description = formData.Description,
            StartDate = formData.StartDate,
            EndDate = formData.EndDate,
            Budget = formData.Budget,
            ClientId = clientResult.Result.Id,
            StatusId = statusResult.Result.Id
        };

        //var projectEntity = formData.MapTo<ProjectEntity>();
        //var statusResult = await _statusService.GetStatusByIdAsync(1);
        //var status = statusResult.Result;
        //projectEntity.StatusId = status!.Id;

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
}
