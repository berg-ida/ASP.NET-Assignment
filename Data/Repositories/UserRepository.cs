using Data.Entities;
using Data.Contexts;
using Data.Interfaces;
using Data.Models;

namespace Data.Repositories;

public interface IUserRepository : IBaseRepository<UserEntity, User>
{

}
public class UserRepository(DataContext context) : BaseRepository<UserEntity, User>(context), IUserRepository
{
}
