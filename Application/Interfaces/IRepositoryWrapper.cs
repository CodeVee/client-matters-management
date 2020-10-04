using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepositoryWrapper
    {
        IClientRepository Client { get; }
        IMatterRepository Matter { get; }
        Task Commit();
    }
}
