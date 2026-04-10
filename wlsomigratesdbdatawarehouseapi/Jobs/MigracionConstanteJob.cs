using Application.CommandsQueries.BonusStatusLogCQ;
using Application.CommandsQueries.CustomerCQ;
using Application.CommandsQueries.HistorialMigracionWSLOCQ;
using Application.CommandsQueries.RealGameEventCQ;
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
            _logger.LogInformation("Job MigracionConstanteJob Iniciado");

            _logger.LogInformation("BonusStatusLog Iniciado");
            var diasBonusesStatusLog = await _mediator.Send(new GetDiasFaltantesMigracionDWQuery { campo = "bonusstatuslog" });
            if(diasBonusesStatusLog != null) {
                foreach(var dia in diasBonusesStatusLog) {
                    await _mediator.Send(new MigrarBonusStatusLogCommand2 { fechaOperacion = dia.fechaoperacion });
                }
            }
            _logger.LogInformation("Customers Iniciado");
            var diasCustomers = await _mediator.Send(new GetDiasFaltantesMigracionDWQuery { campo = "customers" });
            if(diasCustomers != null) {
                foreach(var dia in diasCustomers) {
                    await _mediator.Send(new MigrarCustomerCommand2 { fechaOperacion = dia.fechaoperacion });
                }
            }
            _logger.LogInformation("RealGameEvents Iniciado");
            var diasRealGameEvent = await _mediator.Send(new GetDiasFaltantesMigracionDWQuery { campo = "realgameevents" });
            if(diasRealGameEvent != null) {
                foreach(var dia in diasRealGameEvent) {
                    await _mediator.Send(new MigrarRealGameEventCommand2 { fechaOperacion = dia.fechaoperacion });
                }
            }
            _logger.LogInformation("Job MigracionConstanteJob Finalizado");
        }
    }
}
