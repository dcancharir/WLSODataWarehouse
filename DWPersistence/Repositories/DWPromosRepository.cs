using Application.IRepositories.DW;
using DWDomain;
using DWPersistence.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWPersistence.Repositories;
public class DWPromosRepository : DWBaseRepository<DWPromos>, IDWPromosRepository {
    private readonly DataWarehouseContext _context;
    public DWPromosRepository(DataWarehouseContext context) : base(context) {
        _context = context;
    }
}
