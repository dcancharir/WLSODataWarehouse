using Application.CommandsQueries.RealGameEventCQ;
using MediatR;
using Quartz;

namespace wlsomigratesdbdatawarehouseapi.Jobs;

public class MigrarRealGameEventsJob : IJob {
    private readonly IServiceProvider _serviceProvider;
    public MigrarRealGameEventsJob(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
    }
    public async Task Execute(IJobExecutionContext context) {
        using(IServiceScope scope = _serviceProvider.CreateScope()) {
            var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var _logger = scope.ServiceProvider.GetRequiredService<ILogger<MigrarRealGameEventsJob>>();
            _logger.LogInformation("Job Migracion RealGameEvents iniciado");
            var resultRealGameEvents = await _mediator.Send(new MigrarRealGameEventCommand3());
            _logger.LogInformation("Job Migracion RealGameEvents terminado");
        }
    }
}
