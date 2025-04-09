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
public class DWRealGameEventRepository : DWBaseRepository<DWRealGameEvent>, IDWRealGameEventRepository {
    private readonly DataWarehouseContext _context;
    public DWRealGameEventRepository(DataWarehouseContext context) : base(context) {
        _context = context;
    }

    public async Task<DWRealGameEvent?> GetLastRecordByDate() {
        return await _context.DWRealGameEvents.OrderByDescending(x=>x.InsDatetime).FirstOrDefaultAsync();
    }
}
