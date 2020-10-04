using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMatterRepository : IGenericRepository<Matter>
    {
        Task<IEnumerable<Matter>> GetAllMattersForClient(string clientCode);
        Task<Matter> GetMatterForClient(string clientCode, string matterCode);
        Task<bool> CodeExist(string code);
        void CreateMatterForClient(Matter matter);
        void UpdateMatterForClient(Matter matter);
        void DeleteMatterForClient(Matter matter);
    }
}
