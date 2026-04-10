using Application.IRepositories.DW;
using DWDomain;
using DWPersistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DWPersistence.Repositories;

public class DWHistorialMigracionWSLORepository : DWBaseRepository<DWHistorialMigracionWSLO>, IDWHistorialMigracionWSLORepository {
    private readonly DataWarehouseContext _context;
    public DWHistorialMigracionWSLORepository(DataWarehouseContext context) : base(context) {
        _context = context;
    }

    public async Task<DWHistorialMigracionWSLO?> FirstOrDefault(Expression<Func<DWHistorialMigracionWSLO, bool>> filter) {
        if(filter == null) {
            return null;
        }
        var result = await _context.DWHistorialMigracionWSLOs.Where(filter).ToListAsync();
        return result.FirstOrDefault();
    }

    public async Task<DWHistorialMigracionWSLO?> GetLastRecord() {
        return await _context.DWHistorialMigracionWSLOs.OrderByDescending(x => x.fechaoperacion)
                        .FirstOrDefaultAsync();
    }

    public async Task UpdateAndSave(DWHistorialMigracionWSLO model) {
       _context.DWHistorialMigracionWSLOs.Update(model);
        await _context.SaveChangesAsync();
    }
}
