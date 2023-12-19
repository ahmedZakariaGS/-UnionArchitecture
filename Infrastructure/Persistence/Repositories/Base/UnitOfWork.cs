using Application.Interfaces.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext RepositoryContext;
        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UnitOfWork(DbContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public Task<int> ExecuteSqlRawAsync(string SPName, IEnumerable<object> spParameters, CancellationToken cancellationToken)
        {
            return RepositoryContext.Database.ExecuteSqlRawAsync(SPName, spParameters, cancellationToken);
        }
        public Task<int> CompleteAsync(CancellationToken cancellationToken)
        {
            return RepositoryContext.SaveChangesAsync(cancellationToken);
        }
        public Task<int> CompleteAsync()
        {
            return RepositoryContext.SaveChangesAsync();
        }

        public IRepositoryBase<T>? Repository<T>() where T : class
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IRepositoryBase<T>;
            }

            IRepositoryBase<T> repo = new RepositoryBase<T>(RepositoryContext);
            repositories.Add(typeof(T), repo);
            return repo;
        }
    }
}
