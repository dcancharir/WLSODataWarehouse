using Application.IRepositories.DW;
using DWDomain;
using DWPersistence.DataBaseContext;

namespace DWPersistence.Repositories;
public class DWStoreTxRepository : DWBaseRepository<DWStoreTx>, IDWStoreTxRepository {
    public DWStoreTxRepository(DataWarehouseContext context) : base(context) {
    }
}
