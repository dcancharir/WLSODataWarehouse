using Application.IRepositories.MySql;
using MySqlDomain;
using MySqlPersistence.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlPersistence.Repositories;
public class ProviderRepository : MySqlBaseRepository<Provider>, IProviderRepository {
    public ProviderRepository(MySqlContext context) : base(context) {
    }
}
