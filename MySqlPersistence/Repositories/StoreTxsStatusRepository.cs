using Application.IRepositories.MySql;
using MySqlDomain;
using MySqlPersistence.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlPersistence.Repositories;
public class StoreTxsStatusRepository : MySqlBaseRepository<StoreTxsStatus>, IStoreTxsStatusRepository {
    private readonly MySqlContext _context;
    public StoreTxsStatusRepository(MySqlContext context) : base(context) {
        _context = context;
    }
}
