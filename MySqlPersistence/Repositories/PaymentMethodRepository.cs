using Application.IRepositories.MySql;
using MySqlDomain;
using MySqlPersistence.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlPersistence.Repositories;
internal class PaymentMethodRepository : MySqlBaseRepository<PaymentMethod>, IPaymentMethodRepository {
    public PaymentMethodRepository(MySqlContext context) : base(context) {
    }
}
