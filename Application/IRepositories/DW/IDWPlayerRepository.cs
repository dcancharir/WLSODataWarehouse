using DWDomain;

namespace Application.IRepositories.DW;
public interface IDWPlayerRepository : IDWBaseRepository<DWPlayer>{
    public Task<DWPlayer?> GetLastRecord();
}
