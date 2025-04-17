using Data.Entities;
using Data.Contexts;
using Data.Interfaces;
using Data.Models;

namespace Data.Repositories;

public interface IStatusRepository : IBaseRepository<StatusEntity, Status>
{

}
public class StatusRepository(DataContext context) : BaseRepository<StatusEntity, Status>(context), IStatusRepository
{
}
