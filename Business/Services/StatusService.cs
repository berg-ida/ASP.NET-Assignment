using Business.Dtos;
using Data.Models;
using Data.Repositories;
using Domain.Extentions;

namespace Business.Services;

public interface IStatusService
{
    Task<StatusResult<Status>> ExistsAsync(string statusName);
    Task<StatusResult<Status>> GetStatusByIdAsync(int id);
    Task<StatusResult<Status>> GetStatusByNameAsync(string statusName);
    Task<StatusResult<IEnumerable<Status>>> GetStatusesAsync();
}

public class StatusService(IStatusRepository statusRepository) : IStatusService
{
    private readonly IStatusRepository _statusRepository = statusRepository;

    public async Task<StatusResult<IEnumerable<Status>>> GetStatusesAsync()
    {
        var result = await _statusRepository.GetAllAsync();
        return result.MapTo<StatusResult<IEnumerable<Status>>>();
    }

    public async Task<StatusResult<Status>> GetStatusByNameAsync(string statusName)
    {
        var result = await _statusRepository.GetAsync(x => x.StatusName == statusName);
        return result!.MapTo<StatusResult<Status>>();
    }

    public async Task<StatusResult<Status>> GetStatusByIdAsync(int id)
    {
        var result = await _statusRepository.GetAsync(x => x.Id == id);
        return result!.MapTo<StatusResult<Status>>();
    }

    public async Task<StatusResult<Status>> ExistsAsync(string statusName)
    {
        var result = await GetStatusByNameAsync(statusName);
        if (result.Succeeded && result.Result != null)
        {
            return result;
        }

        return new StatusResult<Status> { Succeeded = false, StatusCode = 404, Error = $"Status '{statusName}' doesn't exist." };

    }
}