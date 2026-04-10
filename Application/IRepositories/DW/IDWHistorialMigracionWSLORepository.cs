using DWDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories.DW;

public interface IDWHistorialMigracionWSLORepository : IDWBaseRepository<DWHistorialMigracionWSLO> {
    Task<DWHistorialMigracionWSLO?> GetLastRecord();
    Task<DWHistorialMigracionWSLO?> FirstOrDefault(Expression<Func<DWHistorialMigracionWSLO, bool>> filter);
    Task UpdateAndSave(DWHistorialMigracionWSLO model);
}
