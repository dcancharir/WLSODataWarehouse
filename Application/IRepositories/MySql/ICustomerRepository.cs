using MySqlDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories.MySql;
public interface ICustomerRepository : IMySqlBaseRepository<Customer> {
    public Task<int> GetTotalRecords();
    public Task<int> GetTotalRecordsByDate(DateTime fecha);
    public Task<IEnumerable<Customer>> GetAllPaginated(int page, int pageSize,DateTime fecha);
    public Task<IEnumerable<Customer>> GetPaginatedByDate(int page, int pageSize,DateTime fecha);
    //public Task<IEnumerable<Customer>> GetAll(int page, int pageSize);
}
