using Application.Interfaces;
using Persistence.Context;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ApplicationContext _context;
        private IClientRepository _client;
        private IMatterRepository _matter;

        public RepositoryWrapper(ApplicationContext context)
        {
            _context = context;
        }
        public IClientRepository Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new ClientRepository(_context);
                }
                return _client;
            }
        }

        public IMatterRepository Matter
        {
            get
            {
                if (_matter == null)
                {
                    _matter = new MatterRepository(_context);
                }
                return _matter;
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}

