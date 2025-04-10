using Application.IRepositories.MySql;
using Microsoft.Extensions.DependencyInjection;
using MySqlPersistence.DataBaseContext;
using MySqlPersistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlPersistence;
public static class DependencyInjection {
    public static void AddMysqlPersistence(this IServiceCollection services) {
        services.AddScoped<MySqlContext>();
        services.AddScoped(typeof(IMySqlBaseRepository<>),typeof(MySqlBaseRepository<>));
        services.AddScoped<IAssociateRepository, AssociateRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IGroupsxRepository, GroupsxRepository>();
        services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IProcessorRepository, ProcessorRepository>();
        services.AddScoped<IProviderRepository, ProviderRepository>();
        services.AddScoped<IRealGameEventRepository, RealGameEventRepository>();
        services.AddScoped<IStoreRepository, StoreRepository>();
        services.AddScoped<IStoreTxRepository, StoreTxRepository>();
        services.AddScoped<ICustomersGroupRepository, CustomersGroupRepository>();
    }
}
