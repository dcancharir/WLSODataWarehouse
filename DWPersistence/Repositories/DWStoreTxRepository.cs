using Application.IRepositories.DW;
using DWDomain;
using DWPersistence.DataBaseContext;

namespace DWPersistence.Repositories;
public class DWStoreTxRepository : DWBaseRepository<DWStoreTx>, IDWStoreTxRepository {
    private readonly DataWarehouseContext _context;
    public DWStoreTxRepository(DataWarehouseContext context) : base(context) {
        _context = context;
    }
}
