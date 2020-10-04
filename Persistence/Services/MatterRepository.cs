using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class MatterRepository : GenericRepository<Matter>, IMatterRepository
    {
        public MatterRepository(ApplicationContext context) : base(context)
        {
        }

        public void CreateMatterForClient(Matter matter)
        {
            Create(matter);
        }

        public void DeleteMatterForClient(Matter matter)
        {
            Delete(matter);
        }

        public async Task<IEnumerable<Matter>> GetAllMattersForClient(string clientCode)
        {
            return await FindByCondition(x => x.ClientCode == clientCode).ToListAsync();
        }

        public async Task<Matter> GetMatterForClient(string clientCode, string code)
        {
            return await FindByCondition(x => x.ClientCode == clientCode && x.MatterCode == code)
                        .FirstOrDefaultAsync();
        }

        public async Task<bool> CodeExist(string code)
        {
            return await FindByCondition(x => x.MatterCode == code).AnyAsync();
        }

        public void UpdateMatterForClient(Matter matter)
        {
            Update(matter);
        }
    }
}
