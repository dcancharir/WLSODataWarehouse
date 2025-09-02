using Application.IRepositories.DW;
using DWPersistence.DataBaseContext;
using DWPersistence.Extensions;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DWPersistence.Repositories;
public class DWBaseRepository<T> : IDWBaseRepository<T> where T : class {
    private readonly DataWarehouseContext _context;
    public DWBaseRepository(DataWarehouseContext context) {
        _context = context;
    }

    public async Task<T> Add(T entity) {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task Delete(T entity) {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAll() {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> GetListByFilter(Expression<Func<T, bool>> filter) {
        var result = filter==null?await _context.Set<T>().ToListAsync() : await _context.Set<T>().Where(filter).ToListAsync();
        return result;
    }

    public async Task<IEnumerable<T>> GetQuery(string[] navigationProperties, Expression<Func<T, bool>> where) {
        var dbQuery = _context
            .Set<T>().Where(where).AsNoTracking();
        if(navigationProperties!=null&&navigationProperties.Length>0) {
            dbQuery = navigationProperties.Aggregate(dbQuery, (current, property) => current.Include(property) );
        }
        if(where != null) {
            dbQuery.Where(where);
        }
        return await dbQuery.ToListAsync();
    }

    public async Task RemoveRange(List<T> entities) {
        _context.Set<T>().RemoveRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task SaveChanges() {
        await _context.SaveChangesAsync();
    }
    public async Task AddIfNotExist(T entity,Expression<Func<T,bool>> predicate) {
        await _context.InsertIfNotExists(entity, predicate);
    }
    public async Task AddRange(List<T> entities) {
        await _context.Set<T>().AddRangeAsync(entities);
    }
    public async Task BulkInsert(List<T> entities) {
        await _context.BulkInsertAsync<T>(entities);
    }
    public async Task BulkSaveChanges() { 
        await _context.BulkSaveChangesAsync();
    }
}
