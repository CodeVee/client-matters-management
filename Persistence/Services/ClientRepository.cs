using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationContext context) : base(context)
        {
        }

        public void CreateClient(Client client)
        {
            Create(client);
        }

        public void DeleteClient(Client client)
        {
            Delete(client);
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetAllClientsWithMatters()
        {
            return await FindAll().Include(x => x.Matters).ToListAsync();
        }

        public async Task<Client> GetClient(string code)
        {
            return await FindByCondition(x => x.ClientCode == code)
                         .FirstOrDefaultAsync();
        }

        public async Task<Client> GetClientWithMatters(string code)
        {
            return await FindByCondition(x => x.ClientCode == code)
                         .Include(x => x.Matters).FirstOrDefaultAsync();
        }

        public async Task<bool> CodeExist(string code)
        {
            return await FindByCondition(x => x.ClientCode == code).AnyAsync();
        }

        public void UpdateClient(Client client)
        {
            Update(client);
        }
    }
}
