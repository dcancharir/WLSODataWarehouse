using Application.IRepositories.DW;
using DWDomain;
using DWPersistence.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWPersistence.Repositories;
public class DWProviderRepository : DWBaseRepository<DWProvider>, IDWProviderRepository {
    public DWProviderRepository(DataWarehouseContext context) : base(context) {
    }
}
