using Application.CommandsQueries.HistorialMigracionWSLOCQ;
using MediatR;
using Quartz;

namespace wlsomigratesdbdatawarehouseapi.Jobs;

[DisallowConcurrentExecution]
public class MigracionConstanteJob : IJob {
    private readonly IServiceProvider _serviceProvider;

    public MigracionConstanteJob(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
    }

    public async Task Execute(IJobExecutionContext context) {
        using(IServiceScope scope = _serviceProvider.CreateScope()) {
            var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var _logger = scope.ServiceProvider.GetRequiredService<ILogger<MigracionConstanteJob>>();

            var diasBonuses = await _mediator.Send(new GetDiasFaltantesMigracionDWQuery { campo = "bonuses"});
            foreach(var dia in diasBonuses) {
                var result = await MigrarBonusesCommand2(dia.fechaoperacion);
                if(result) {
                    await _mediator.Send(new UpdateMigracionDWCommand { campo = "afluenciahora", fecha = dia.fechaoperacion });
                }
            }
        }
    }
}
