using Microsoft.Extensions.DependencyInjection;
using Persistence.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence;
public static class DependencyInjection {
    public static void AddPersistence(this IServiceCollection services) {
        services.AddSingleton<MySqlContext>();
        services.AddSingleton<DataWarehouseContext>();
    }
}
