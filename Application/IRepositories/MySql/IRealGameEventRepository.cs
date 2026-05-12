using MySqlDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories.MySql;
public interface IRealGameEventRepository : IMySqlBaseRepository<RealGameEvent> {
    Task<IEnumerable<RealGameEvent>> GetPaginatedByDates(int page, int pageSize, DateTime insDateTime);
    Task<int> GetTotalRecordsByDate(DateTime insDateTime);
    Task<IEnumerable<RealGameEvent>> GetByFechaOperacion(DateTime fechaOperacion);
    Task<int> GetTotalRecordsById(ulong id);
    Task<IEnumerable<RealGameEvent>> GetPaginatedById(int page, int pageSize, ulong id);
}
