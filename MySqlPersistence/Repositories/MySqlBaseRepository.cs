using Application.IRepositories.MySql;
using Microsoft.EntityFrameworkCore;
using MySqlPersistence.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MySqlPersistence.Repositories;
public class MySqlBaseRepository<T> : IMySqlBaseRepository<T> where T : class {
    private readonly MySqlContext _context;
    public MySqlBaseRepository(MySqlContext context) {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAll() {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> GetListByFilter(Expression<Func<T, bool>> filter) {
        var result = filter == null? await _context.Set<T>().ToListAsync() : await _context.Set<T>().Where(filter).ToListAsync();
        return result;
    }

    public async Task<IEnumerable<T>> GetQuery(string[] navigationProperties, Expression<Func<T, bool>> where) {
        var dbQuery = _context
            .Set<T>().Where(where).AsNoTracking();
        if(navigationProperties != null && navigationProperties.Length > 0) {
            dbQuery = navigationProperties.Aggregate(dbQuery, (current, property) => current.Include(property));
        }
        if(where != null) {
            dbQuery.Where(where);
        }
        return await dbQuery.ToListAsync();
    }
}
