using Application.IRepositories.DW;
using DWDomain;
using DWPersistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWPersistence.Repositories;
public class DWPlayerRepository : DWBaseRepository<DWPlayer>, IDWPlayerRepository {
    private readonly DataWarehouseContext _context;
    public DWPlayerRepository(DataWarehouseContext context) : base(context) {
        _context = context;
    }
    public async Task<DWPlayer?> GetLastRecord() {
        return await _context.DWPlayers.LastOrDefaultAsync();
    }
}
