using System.Linq.Expressions;

namespace Application.IRepositories.MySql;
public interface IMySqlBaseRepository<T> where T : class {
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetListByFilter(Expression<Func<T, bool>> filter);
    Task<IEnumerable<T>> GetQuery(string[] navigationProperties, Expression<Func<T, bool>> where);
    Task<long> GetCountAll();

}
