using Data.Entities;
using Data.Contexts;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface IProjectRepository : IBaseRepository<ProjectEntity, Project>
{
    public Task<RepositoryResult<IEnumerable<Project>>> GetProjectsAsync();
}
public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity, Project>(context), IProjectRepository
{
    public async Task<RepositoryResult<IEnumerable<Project>>> GetProjectsAsync()
    {
        var entities = await _context.Projects.Include(i => i.Client).Include(i => i.Status).ToListAsync();

        var projects = entities.Select(ToModel);
        return new RepositoryResult<IEnumerable<Project>> { Succeeded = true, Result = projects };
    }

    public static Project ToModel(ProjectEntity entity)
    {
        return new Project
        {
            Id = entity.Id,
            ProjectName = entity.ProjectName,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Budget = entity.Budget,
            Client = entity.Client != null ? new Client
            {
                Id = entity.Client.Id,
                ClientName = entity.Client.ClientName
            } : null,
            Status = entity.Status != null ? new Status
            {
                Id = entity.Status.Id,
                StatusName = entity.Status.StatusName
            } : null
        };
    }
}
