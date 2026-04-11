using Application.IRepositories.DW;
using DWDomain;
using DWPersistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using MySqlDomain;
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
    public async Task<IEnumerable<DWCustomer>> GetByFechaOperacion(DateTime fechaOperacion) {
        var inicio = fechaOperacion.Date;
        var fin = inicio.AddDays(1);

        FormattableString query = $@"
           SELECT  
    [associateId],  
    [storeId],  
    [playerId],  
    [username],  
    [email],  
    [firstName],  
    [lastName],  
    [phone],  
    [active],  
    [verified],  
    [excluded],  
    [regDatetime],  
    [birthdate],  
    [addressDept],  
    [addressProv],  
    [addressDist], 
    [address],  
    CAST([identId] AS VARCHAR(250)) AS identId,  
    [identification],  
    CAST([ip] AS VARCHAR(39)) AS ip,
    [lastLoginTimestamp],
    CAST([countryId] AS VARCHAR(50)) AS countryId,
    [regDate],
    [icCode],
    [phoneChecked],
    [emailChecked],
    [city],
    [lastLoginDatetime],
    [updDatetime],
    [regTimestamp],
    [gender]
FROM [Customers]
WHERE [regDate] >= {inicio} AND [regDate] < {fin}
        ";
        var result = await _context.DWCustomers.FromSql(query).AsNoTracking().ToListAsync();
        return result;
    }
}
