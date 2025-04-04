using MySqlDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories.MySql;
public interface ICustomerRepository : IMySqlBaseRepository<Customer> {
}
