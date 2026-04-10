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
public class DWBonusStatusLogRepository : DWBaseRepository<DWBonusStatusLog>, IDWBonusStatusLogRepository {
    private readonly DataWarehouseContext _context;
    public DWBonusStatusLogRepository(DataWarehouseContext context) : base(context) {
        _context = context;
    }

    public async Task RemoveByDateAsync(DateTime fechaOperacion) {
        DateOnly fechaOperacionDate = DateOnly.FromDateTime(fechaOperacion);
        var items = await _context.DWBonusStatusLogs.Where(x => x.SetDate == fechaOperacionDate).ToListAsync();
        if(items.Count > 0) {
            _context.DWBonusStatusLogs.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
