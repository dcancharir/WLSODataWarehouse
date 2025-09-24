using Application.IRepositories.MySql;
using DWDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories.DW;
public interface IDWBonusesRepository : IDWBaseRepository<DWBonuse>{
    public Task<DWBonuse?> GetLastRecordByInsTimestamp();
}
