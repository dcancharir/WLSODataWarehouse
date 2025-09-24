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
public class DWUserRepository : DWBaseRepository<DWUser>, IDWUserRepository {
    private readonly DataWarehouseContext _context;
    public DWUserRepository(DataWarehouseContext context) : base(context) {
        _context = context;
    }
    public async Task<DWUser?> GetLastRecord() {
        return await _context.DWUsers.OrderByDescending(x => x.UserId).FirstOrDefaultAsync();
    }
}
