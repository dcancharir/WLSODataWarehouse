using DWDomain;

namespace Application.IRepositories.DW;
public interface IDWCustomerRepository : IDWBaseRepository<DWCustomer>{
    public Task<DWCustomer?> GetLastRecordByRegDateTime();
}
