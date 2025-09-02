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
public class DWCustomerRepository : DWBaseRepository<DWCustomer>, IDWCustomerRepository {
    private readonly DataWarehouseContext _context;
    public DWCustomerRepository(DataWarehouseContext context) : base(context) {
        _context = context;
    }
    public async Task<DWCustomer?> GetLastRecordByRegDateTime() {
        return await _context.DWCustomers.OrderByDescending(x=>x.RegDatetime).FirstOrDefaultAsync();
    }

    public async Task<int> GetTotalRecords() {
        return await _context.DWCustomers.CountAsync();
    }
}
