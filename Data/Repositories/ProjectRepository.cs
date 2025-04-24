using Data.Entities;
using Data.Contexts;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Diagnostics;

namespace Data.Repositories;

public interface IProjectRepository : IBaseRepository<ProjectEntity, Project>
{
    Task<RepositoryResult<bool>> AddAsync(ProjectEntity entity, ClientEntity client, StatusEntity status);
    Task<RepositoryResult<IEnumerable<Project>>> GetProjectsAsync();
    new Task<ProjectEntity?> GetAsync(Expression<Func<ProjectEntity, bool>> predicate, Func<IQueryable<ProjectEntity>, IIncludableQueryable<ProjectEntity, object>>? include = null);
}
public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity, Project>(context), IProjectRepository
{
    public virtual async Task<RepositoryResult<bool>> AddAsync(ProjectEntity entity, ClientEntity client, StatusEntity status)
    {
        if (entity == null)
        {
            return new RepositoryResult<bool> { Succeeded = false, StatusCode = 400, Error = "Entity cannot be null." };
        }

        try
        {
            _table.Add(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { Succeeded = true, StatusCode = 201 };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new RepositoryResult<bool> { Succeeded = false, StatusCode = 500, Error = ex.Message };
        }
    }

    public async Task<RepositoryResult<IEnumerable<Project>>> GetProjectsAsync()
    {
        var entities = await _context.Projects.Include(i => i.Client).Include(i => i.Status).ToListAsync();

        var projects = entities.Select(ToModel);
        return new RepositoryResult<IEnumerable<Project>> { Succeeded = true, Result = projects };
    }

    public static Project ToModel(ProjectEntity entity)
    {
        if (entity == null) return null!;

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
                ClientName = entity.Client.ClientName ?? "Unknown Client"
            } : new Client { ClientName = "Unknown Client" },
            Status = entity.Status != null ? new Status
            {
                Id = entity.Status.Id,
                StatusName = entity.Status.StatusName ?? "Not yet started"
            } : new Status { StatusName = "Not yet started" }
        };
    }

    new public virtual async Task<ProjectEntity?> GetAsync(Expression<Func<ProjectEntity, bool>> predicate, Func<IQueryable<ProjectEntity>, IIncludableQueryable<ProjectEntity, object>>? include = null)
    {
        IQueryable<ProjectEntity> query = _context.Projects;

        if (include != null)
            query = include(query);

        return await query.FirstOrDefaultAsync(predicate);
    }

}
