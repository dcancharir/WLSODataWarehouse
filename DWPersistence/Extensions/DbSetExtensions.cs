using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DWPersistence.Extensions;
public static class DbSetExtensions {
    public static async Task InsertIfNotExists<TEntity>
        (this DbContext context, TEntity entity,Expression<Func<TEntity,bool>> predicate) 
        where TEntity : class {
        if(!await context.Set<TEntity>().AnyAsync(predicate)) {
            await context.Set<TEntity>().AddAsync(entity);
        }
    }
}
