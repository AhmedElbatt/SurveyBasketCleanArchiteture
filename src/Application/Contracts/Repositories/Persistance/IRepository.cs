using System.Linq.Expressions;

namespace Application.Contracts.Repositories.Persistance;
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<T> GetAsync(Expression<Func<T, bool>>? predicate = null,
                                  bool disableTracking = true,
                                   CancellationToken cancellationToken = default,
                                  params Expression<Func<T, object>>[] includeProperties);
    Task<IEnumerable<T>> GetListAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null,
                                  Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                  bool disableTracking = true,
                                  CancellationToken cancellationToken = default,
                                  params Expression<Func<T, object>>[] includeProperties);

    //Task<PageResultSet<T>> GetListAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize);
    //Task<PageResultSet<T>> GetListAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? predicate = null,
    //                      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
    //                      bool disableTracking = true,
    //                      params Expression<Func<T, object>>[] includeProperties);

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

    Task<List<T>> AddRangeAsync(List<T> entities, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}
