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
public class DWBonuseRepository : DWBaseRepository<DWBonuse>, IDWBonusesRepository {
    private readonly DataWarehouseContext _context;
    public DWBonuseRepository(DataWarehouseContext context) : base(context) {
        _context = context;
    }
    public async Task<DWBonuse?> GetLastRecordByInsTimestamp() {
        return await _context.DWBonuses.OrderByDescending(x => x.InsTimestamp).FirstOrDefaultAsync();
    }
}
