using Application.IRepositories.DW;
using DWDomain;
using DWPersistence.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWPersistence.Repositories;
public class DWRealGameEventRepository : DWBaseRepository<DWRealGameEvent>, IDWRealGameEventRepository {
    public DWRealGameEventRepository(DataWarehouseContext context) : base(context) {
    }
}
