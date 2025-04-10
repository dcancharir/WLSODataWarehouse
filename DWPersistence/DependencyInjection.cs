using Application.IRepositories.DW;
using DWPersistence.DataBaseContext;
using DWPersistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DWPersistence;
public static class DependencyInjection {
    public static void AddDWPersistence(this IServiceCollection services) {
        services.AddScoped<DataWarehouseContext>();

        services.AddScoped(typeof(IDWBaseRepository<>), typeof(DWBaseRepository<>));
        services.AddScoped<IDWAssociateRepository, DWAssociateRepository>();
        services.AddScoped<IDWBrandRepository, DWBrandRepository>();
        services.AddScoped<IDWCustomerRepository, DWCustomerRepository>();
        services.AddScoped<IDWGameRepository, DWGameRepository>();
        services.AddScoped<IDWGroupsxRepository, DWGroupsxRepository>();
        services.AddScoped<IDWPaymentMethodRepository, DWPaymentMethodRepository>();
        services.AddScoped<IDWPlayerRepository, DWPlayerRepository>();
        services.AddScoped<IDWProcessorRepository, DWProcessorRepository>();
        services.AddScoped<IDWProviderRepository, DWProviderRepository>();
        services.AddScoped<IDWRealGameEventRepository, DWRealGameEventRepository>();
        services.AddScoped<IDWStoreRepository, DWStoreRepository>();
        services.AddScoped<IDWStoreTxRepository, DWStoreTxRepository>();
        services.AddScoped<IDWCustomersGroupRepository,DWCustomerGroupRepository>();
    }
}
