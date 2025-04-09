using MySqlDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories.MySql;
public interface IPlayerRepository : IMySqlBaseRepository<Player> {
    public Task<int> GetTotalRecordsById(uint id);
    public Task<IEnumerable<Player>> GetPaginatedById(int page, int pageSize, uint id);
}
