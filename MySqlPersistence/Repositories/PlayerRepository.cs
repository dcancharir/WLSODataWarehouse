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
public class PlayerRepository : MySqlBaseRepository<Player>, IPlayerRepository {
    private readonly MySqlContext _context;
    public PlayerRepository(MySqlContext context) : base(context) {
        _context = context;
    }

    public async Task<IEnumerable<Player>> GetPaginatedById(int page, int pageSize, uint id) {
        return await _context.Players.Where(x => x.PlayerId >= id).Skip(page).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetTotalRecordsById(uint id) {
        return await _context.Players.Where(x=>x.PlayerId >= id).CountAsync();
    }
}
