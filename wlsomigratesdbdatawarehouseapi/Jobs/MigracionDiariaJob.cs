using Application.CommandsQueries.AssociateCQ;
using MediatR;
using Quartz;
namespace wlsomigratesdbdatawarehouseapi.Jobs;
[DisallowConcurrentExecution]
public class MigracionDiariaJob : IJob{
    private readonly IServiceProvider _serviceProvider;

    public MigracionDiariaJob(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
    }
    public async Task Execute(IJobExecutionContext context) {
        using(IServiceScope scope = _serviceProvider.CreateScope()) {
            var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var _logger = scope.ServiceProvider.GetRequiredService<ILogger<MigracionDiariaJob>>();
            await AssociateMigration(_mediator);
        }
    }
    internal static async Task AssociateMigration(IMediator _mediator) {
        var respuesta = await _mediator.Send(new MigrarAssociateCommand() { });
    }
}
