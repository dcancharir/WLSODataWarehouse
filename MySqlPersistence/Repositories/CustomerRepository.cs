﻿using Application.IRepositories.MySql;
using MySqlDomain;
using MySqlPersistence.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlPersistence.Repositories;
public class CustomerRepository : MySqlBaseRepository<Customer>, ICustomerRepository {
    public CustomerRepository(MySqlContext context) : base(context) {
    }
}
