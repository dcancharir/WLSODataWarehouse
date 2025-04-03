using Application.IRepositories.MySql;
using Domain.MySql;
using Persistence.DataBaseContext;

namespace Persistence.Repositories.MySql;
public class AssociateRepository : MySqlBaseRepository<Associate> ,IAssociateRepository {
    private readonly MySqlContext _context;
    public AssociateRepository(MySqlContext context) : base(context) {
        _context = context;
    }
}
