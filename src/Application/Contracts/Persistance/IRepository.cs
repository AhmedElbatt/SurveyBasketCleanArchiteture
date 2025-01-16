using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Contracts.Persistance;
public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
}
