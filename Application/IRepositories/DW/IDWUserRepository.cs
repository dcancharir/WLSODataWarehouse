using DWDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories.DW;
public interface IDWUserRepository : IDWBaseRepository <DWUser>{
    public Task<DWUser?> GetLastRecord();
}
