using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace Application;
public static class DependencyInjection {
    public static void AddApplication(this IServiceCollection services) {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
    }
}
