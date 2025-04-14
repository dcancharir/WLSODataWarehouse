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
public class CustomerRepository : MySqlBaseRepository<Customer>, ICustomerRepository {
    private readonly MySqlContext _context;
    public CustomerRepository(MySqlContext context) : base(context) {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllPaginated(int page, int pageSize, DateTime fecha) {
        int offset = (page - 1) * pageSize;
        //string dateStr = fecha.ToString("yyyy-MM-dd HH:mm:ss");
        FormattableString query = $@"
    SELECT  
`associateId`,  
`storeId`,  
`playerId`,  
`username`,  
`email`,  
`firstName`,  
`lastName`,  
`phone`,  
`active`,  
`verified`,  
`excluded`,  
`regDatetime`,  
`birthdate`,  
`addressDept`,  
`addressProv`,  
`addressDist`, 
`address`,  
CAST(`identId` as CHAR(250)) as identId,  
`identification` 
FROM `Customers` where `regDatetime` >= {fecha} order by `regDatetime` asc LIMIT {pageSize} offset {offset};
";
        var result = await _context.Customers.FromSql(query).AsNoTracking().ToListAsync();
        return result;
    }

    //public async Task<IEnumerable<Customer>> GetAllPaginated(int page, int pageSize) {
    //    return await _context.Customers.Skip(page).Take(pageSize).ToListAsync();
    //}

    public async Task<IEnumerable<Customer>> GetPaginatedByDate(int page, int pageSize, DateTime fecha) {
        return await _context.Customers.Where(x=>x.RegDatetime >= fecha).Skip(page).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetTotalRecords() {
        return await _context.Customers.CountAsync();
    }

    public async Task<int> GetTotalRecordsByDate(DateTime fecha) {
        return await _context.Customers.Where(x => x.RegDatetime >= fecha).CountAsync();
    }
}
