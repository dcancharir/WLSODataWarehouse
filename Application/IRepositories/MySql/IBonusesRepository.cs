using MySqlDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories.MySql;
public interface IBonusesRepository : IMySqlBaseRepository<Bonuse> {
    Task<IEnumerable<Bonuse>> GetPaginatedByTimestamp(int page, int pageSize, long insTimestamp);
    Task<int> GetTotalRecordsByTimestamp(long insTimestamp);
}
