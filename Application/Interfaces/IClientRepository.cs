using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<IEnumerable<Client>> GetAllClients();
        Task<IEnumerable<Client>> GetAllClientsWithMatters();
        Task<Client> GetClient(string code);
        Task<Client> GetClientWithMatters(string code);
        Task<bool> CodeExist(string code);
        void CreateClient(Client client);
        void UpdateClient(Client client);
        void DeleteClient(Client client);
    }
}
