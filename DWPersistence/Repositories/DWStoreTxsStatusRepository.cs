using Application.IRepositories.DW;
using DWDomain;
using DWPersistence.DataBaseContext;

namespace DWPersistence.Repositories;
public class DWStoreTxsStatusRepository : DWBaseRepository<DWStoreTxsStatus>, IDWStoreTxsStatusRepository {
    private readonly DataWarehouseContext _context;
    public DWStoreTxsStatusRepository(DataWarehouseContext context) : base(context) {
        _context = context;
    }
}
