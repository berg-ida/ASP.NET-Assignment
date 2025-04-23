using Data.Entities;
using Data.Contexts;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public interface IStatusRepository : IBaseRepository<StatusEntity, Status>
{
    new Task<StatusEntity> AddAsync(StatusEntity entity);
    Task<StatusEntity?> GetAsync(Expression<Func<StatusEntity, bool>> predicate);
}

public class StatusRepository(DataContext context) : BaseRepository<StatusEntity, Status>(context), IStatusRepository
{
    public async Task<StatusEntity?> GetAsync(Expression<Func<StatusEntity, bool>> predicate)
    {
        return await _context.Statuses.FirstOrDefaultAsync(predicate);
    }

    new public async Task<StatusEntity> AddAsync(StatusEntity entity)
    {
        await _context.Statuses.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
