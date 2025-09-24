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
public class BonusesRepository : MySqlBaseRepository<Bonuse>, IBonusesRepository {
    private readonly MySqlContext _context;
    public BonusesRepository(MySqlContext context) : base(context) {
        _context = context;
    }
    public async Task<IEnumerable<Bonuse>> GetPaginatedByTimestamp(int page, int pageSize, long insTimestamp) {
        return await _context.Bonuses.Where(x => x.InsTimestamp >= insTimestamp).OrderBy(x => x.InsTimestamp).Skip(page).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetTotalRecordsByTimestamp(long insTimestamp) {
        return await _context.Bonuses.Where(x => x.InsTimestamp >= insTimestamp).CountAsync();
    }
}
