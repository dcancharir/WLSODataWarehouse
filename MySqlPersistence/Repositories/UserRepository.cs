using Application.IRepositories.MySql;
using Microsoft.EntityFrameworkCore;
using MySqlDomain;
using MySqlPersistence.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlPersistence.Repositories;
public class UserRepository : MySqlBaseRepository<User>, IUserRepository {
    private readonly MySqlContext _context;
    public UserRepository(MySqlContext context) : base(context) {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetPaginatedById(int page, int pageSize, uint id) {
        return await _context
                        .Users
                        .Where(x=>x.UserId >= id)
                        .OrderBy(x=>x.UserId)
                        .Skip(page)
                        .Take(pageSize)
                        .ToListAsync();
    }

    public async Task<int> GetTotalRecordsById(uint id) {
        return await _context.Users.Where(x=>x.UserId >= id).CountAsync();
    }
}
