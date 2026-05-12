using DWDomain;

namespace Application.IRepositories.DW;
public interface IDWRealGameEventRepository : IDWBaseRepository<DWRealGameEvent>{
    public Task<DWRealGameEvent?> GetLastRecordByDate();
    public Task<DWRealGameEvent?> GetLastRecord();
}
