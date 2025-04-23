using Business.Dtos;
using Data.Entities;
using Data.Models;
using Data.Repositories;
using Domain.Extentions;

namespace Business.Services;

public interface IClientService
{
    Task<ClientResult<Client>> CreateClientAsync(string clientName);
    Task<ClientResult<Client>> GetClientByNameAsync(string clientName);
    Task<ClientResult> GetClientsAsync();
}

public class ClientService(IClientRepository clientRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;
    public async Task<ClientResult> GetClientsAsync()
    {
        var result = await _clientRepository.GetAllAsync();
        return result.MapTo<ClientResult>();
    }

    public async Task<ClientResult<Client>> GetClientByNameAsync(string clientName)
    {
        var result = await _clientRepository.GetAsync(x => x.ClientName == clientName);
        return result!.MapTo<ClientResult<Client>>();
    }

    public async Task<ClientResult<Client>> CreateClientAsync(string clientName)
    {
        try
        {
            var existingClient = await _clientRepository.GetAsync(x => x.ClientName == clientName);
            if (existingClient != null)
            {
                return new ClientResult<Client> { Succeeded = true, Result = existingClient.MapTo<Client>() };
            }

            var newClient = new ClientEntity
            {
                Id = Guid.NewGuid().ToString(),
                ClientName = clientName
            };

            await _clientRepository.AddAsync(newClient);
            return new ClientResult<Client> { Succeeded = true, Result = newClient.MapTo<Client>() };
        }
        catch (Exception ex)
        {
            return new ClientResult<Client> { Succeeded = false, Error = ex.Message };
        }
    }
}
