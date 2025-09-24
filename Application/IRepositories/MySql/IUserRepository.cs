using MySqlDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories.MySql;
public interface IUserRepository : IMySqlBaseRepository<User>{
    public Task<int> GetTotalRecordsById(uint id);
    public Task<IEnumerable<User>> GetPaginatedById(int page, int pageSize, uint id);
}
