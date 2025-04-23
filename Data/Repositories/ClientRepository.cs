using Data.Entities;
using Data.Contexts;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public interface IClientRepository : IBaseRepository<ClientEntity, Client>
{
    new Task<ClientEntity> AddAsync(ClientEntity entity);
    Task<ClientEntity?> GetAsync(Expression<Func<ClientEntity, bool>> predicate);
}

public class ClientRepository(DataContext context) : BaseRepository<ClientEntity, Client>(context), IClientRepository
{
    public async Task<ClientEntity?> GetAsync(Expression<Func<ClientEntity, bool>> predicate)
    {
        return await _context.Clients.FirstOrDefaultAsync(predicate);
    }

    new public async Task<ClientEntity> AddAsync(ClientEntity entity)
    {
        await _context.Clients.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
