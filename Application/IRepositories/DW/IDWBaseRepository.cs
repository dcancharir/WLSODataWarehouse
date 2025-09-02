using System.Linq.Expressions;

namespace Application.IRepositories.DW;
public interface IDWBaseRepository<T> where T:class {
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetListByFilter(Expression<Func<T, bool>> filter);
    Task<IEnumerable<T>> GetQuery(string[] navigationProperties, Expression<Func<T, bool>> where);
    Task SaveChanges();
    Task<T> Add(T entity);
    Task Delete(T entity);
    Task RemoveRange(List<T> entities);
    Task AddIfNotExist(T entity, Expression<Func<T, bool>> predicate);
    Task AddRange(List<T> entities);
    Task BulkInsert(List<T> entities);
    Task BulkSaveChanges();
}
