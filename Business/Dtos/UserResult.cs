using Data.Models;

namespace Business.Dtos;

public class UserResult : ServiceResult
{
    public IEnumerable<User>? Result { get; set; }
}
