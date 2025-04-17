using Data.Models;

namespace Business.Dtos;

public class ClientResult : ServiceResult
{
    public IEnumerable<Client>? Result { get; set; }
}

public class ClientResult<T> : ServiceResult
{
    public T? Result { get; set; }
}