using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Base
{
    public interface IUnitOfWork
    {
        IRepositoryBase<T> Repository<T>() where T : class;
        Task<int> CompleteAsync(CancellationToken cancellationToken);
        Task<int> CompleteAsync();
        Task<int> ExecuteSqlRawAsync(string SPName, IEnumerable<object> spParameters, CancellationToken cancellationToken);
    }
}
