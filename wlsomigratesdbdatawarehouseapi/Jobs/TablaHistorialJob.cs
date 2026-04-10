using Application.CommandsQueries.AssociateCQ;
using Application.CommandsQueries.HistorialMigracionWSLOCQ;
using Application.ViewModels;
using MediatR;
using Quartz;

namespace wlsomigratesdbdatawarehouseapi.Jobs;

[DisallowConcurrentExecution]
public class TablaHistorialJob : IJob {
    private readonly IServiceProvider _serviceProvider;
    public TablaHistorialJob(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
    }
    public async Task Execute(IJobExecutionContext context) {
        using(IServiceScope scope = _serviceProvider.CreateScope()) {
            var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var _logger = scope.ServiceProvider.GetRequiredService<ILogger<TablaHistorialJob>>();
            _logger.LogInformation($"Job TablaHistorialJob para creacion de fecha de operacion iniciado");
            var _configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            string fechaBaseStr = _configuration["FechaBase"] ?? string.Empty;
            DateTime? fechaBase = ConvertirFecha(fechaBaseStr);

            var fechaActual = DateTime.Now;
            //si estamos 18, deberia migrar data del dia 17, ya que la del 18 aun no esta completa 
            var fechaOperacion = fechaActual.AddDays(-1);


            List<DWHistorialMigracionWSLOViewModel> listaInsertar;
            var existeFechaOperacion = _mediator.Send(new GetLastRecordHistorialMigracionDWQuery());
            if(existeFechaOperacion.Result != null) {
                fechaBase = existeFechaOperacion.Result.fechaoperacion;
                listaInsertar = ObtenerListadoFechas(Convert.ToDateTime(fechaBase).AddDays(1), fechaOperacion);

            } else {
                listaInsertar = ObtenerListadoFechas(Convert.ToDateTime(fechaBase), fechaOperacion);
            }

            var fechasCreadas = await _mediator.Send(new CrearHistorialMigracionDWCommand() { registro = listaInsertar});
        }
    }
    internal List<DWHistorialMigracionWSLOViewModel> ObtenerListadoFechas(DateTime fechaInicial, DateTime fechaFinal) {
        List<DWHistorialMigracionWSLOViewModel> fechas = new List<DWHistorialMigracionWSLOViewModel>();
        DateTime fechaActual = fechaInicial;
        while(fechaActual <= fechaFinal) {
            fechas.Add(new DWHistorialMigracionWSLOViewModel {
                fechaoperacion = fechaActual,
                bonusstatuslog = 0,
                customers = 0,
                realgameevents = 0,
            });
            fechaActual = fechaActual.AddDays(1);
        }
        return fechas;
    }
    internal DateTime? ConvertirFecha(string fecha) {
        if(string.IsNullOrEmpty(fecha))
            return null;
        DateTime result;
        try {
            var strArray = fecha.Replace("/", "-").Split('-');
            if(strArray.Count() == 3) {
                var dia = Convert.ToInt32(strArray[2]);
                var mes = Convert.ToInt32(strArray[1]);
                var anio = Convert.ToInt32(strArray[0]);
                result = new DateTime(anio, mes, dia);
                return result;
            }
            return null;
        } catch(Exception) {
            return null;
        }
    }
}
