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
public class RealGameEventRepository : MySqlBaseRepository<RealGameEvent>, IRealGameEventRepository {
    private readonly MySqlContext _context;
    public RealGameEventRepository(MySqlContext context) : base(context) {
        _context = context;
    }

    public async Task<IEnumerable<RealGameEvent>> GetPaginatedByDates(int page, int pageSize, DateTime insDateTime) {
        return await _context.RealGameEvents.Where(x => x.InsDatetime >= insDateTime).OrderBy(x=>x.InsDatetime).Skip(page).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetTotalRecordsByDate(DateTime insDateTime) {
        return await _context.RealGameEvents.Where(x=>x.InsDatetime >= insDateTime).CountAsync();
    }
}
