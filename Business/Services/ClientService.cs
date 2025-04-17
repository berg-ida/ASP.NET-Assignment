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
        return result.MapTo<ClientResult<Client>>();
    }

    public async Task<ClientResult<Client>> CreateClientAsync(string clientName)
    {
        var existingClient = await GetClientByNameAsync(clientName);
        if (existingClient.Succeeded && existingClient.Result != null)
        {
            return existingClient;
        }

        var client = new ClientEntity
        {
            ClientName = clientName,
            Id = Guid.NewGuid().ToString(),
        };
        var result = await _clientRepository.AddAsync(client);

        if (!result.Succeeded)
        {
            return new ClientResult<Client> { Succeeded = false, Error = result.Error };
        }

        return new ClientResult<Client> { Succeeded = true, Result = new Client { Id = client.Id, ClientName = client.ClientName } };
    }
}
