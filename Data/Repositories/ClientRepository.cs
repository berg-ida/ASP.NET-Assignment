using Data.Entities;
using Data.Contexts;
using Data.Interfaces;
using Data.Models;

namespace Data.Repositories;

public interface IClientRepository : IBaseRepository<ClientEntity, Client>
{

}

public class ClientRepository(DataContext context) : BaseRepository<ClientEntity, Client>(context), IClientRepository
{

}
