using DWDomain;

namespace Application.IRepositories.DW;
public interface IDWCustomerRepository : IDWBaseRepository<DWCustomer>{
    public Task<DWCustomer?> GetLastRecordByRegDateTime();
    public Task<int> GetTotalRecords();
    public Task<IEnumerable<DWCustomer>> GetByFechaOperacion(DateTime fechaOperacion);
}
