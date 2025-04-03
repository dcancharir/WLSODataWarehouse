using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories.MySql;
public interface IMySqlBaseRepository<T> where T : class {
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetListByFilter(Expression<Func<T, bool>> filter);
    IQueryable<T> GetQuery(string[] navigationProperties, Expression<Func<T, bool>> where);
}
