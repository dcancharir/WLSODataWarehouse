using Application.IRepositories.MySql;
using MySqlDomain;
using MySqlPersistence.DataBaseContext;

namespace MySqlPersistence.Repositories;
public class AssociateRepository : MySqlBaseRepository<Associate> ,IAssociateRepository {
    private readonly MySqlContext _context;
    public AssociateRepository(MySqlContext context) : base(context) {
        _context = context;
    }
}
