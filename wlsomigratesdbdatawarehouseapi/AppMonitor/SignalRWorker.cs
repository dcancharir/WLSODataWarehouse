using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using wlsomigratesdbdatawarehouseapi.AppMonitor;

namespace gladconNewWebOnline.Workers.AppMonitor;

public class SignalRWorker : BackgroundService {
    private HubConnection _connection;
    private readonly ILogger<SignalRWorker> _logger;
    private readonly string _url;
    private readonly AppMonitorSettings _appMonitorSettings;

    public SignalRWorker(ILogger<SignalRWorker> logger, IOptions<AppMonitorSettings> options) {
        _logger = logger;
        _appMonitorSettings = options.Value;
        _url = string.Format(_appMonitorSettings.UrlFormat, _appMonitorSettings.Host, _appMonitorSettings.AppId, _appMonitorSettings.ApiKey);
    }

    // Backoff: 5s, 10s, 30s, 60s — luego se mantiene en 60s
    private static readonly TimeSpan[] RetryDelays = new[] {
        TimeSpan.FromSeconds(5),
        TimeSpan.FromSeconds(10),
        TimeSpan.FromSeconds(30),
        TimeSpan.FromSeconds(60)
    };

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        if(!_appMonitorSettings.Enabled) {
            _logger.LogWarning("SignalR Monitor deshabilitado. Worker no iniciará.");
            return;
        }

        _connection = new HubConnectionBuilder()
            .WithUrl(_url)
            .WithAutomaticReconnect(RetryDelays)
            .Build();

        RegisterEvents();
        RegisterHandlers();

        await ConnectWithBackoffAsync(stoppingToken);
    }

    // ── Eventos del ciclo de vida de la conexión ──────────────────
    private void RegisterEvents() {
        _connection!.Reconnecting += error => {
            _logger.LogWarning("Conexión perdida. Reconectando... Error: {Error}", error?.Message);
            return Task.CompletedTask;
        };

        _connection.Reconnected += async connectionId => {
            _logger.LogInformation("Conexión restablecida. ID: {ConnectionId}", connectionId);
            await NotificarEstadoAsync("online");
        };

        _connection.Closed += async error => {
            _logger.LogError("Conexión cerrada definitivamente. Error: {Error}", error?.Message);
            // AutomaticReconnect agotó sus intentos — reconectamos manualmente
            if(!_cts.IsCancellationRequested)
                await ConnectWithBackoffAsync(_cts.Token);
        };
    }

    // ── Handlers: mensajes que llegan desde el Hub ────────────────
    private void RegisterHandlers() {
        // Evento genérico de monitoreo
        _connection!.On<MonitorEvent>("ReceiveMonitorEvent", async monitorEvent => {
            _logger.LogInformation("Evento recibido: {Tipo} - {Descripcion}", monitorEvent.Tipo, monitorEvent.Descripcion);
            await ProcesarEventoAsync(monitorEvent);
        });

        // Comando del servidor
        _connection.On<string>("ExecuteCommand", async command => {
            _logger.LogInformation("Comando recibido: {Command}", command);
            await ProcesarComandoAsync(command);
        });

        // Ping del servidor — responder inmediatamente
        _connection.On("Ping", async () => {
            _logger.LogDebug("Ping recibido, respondiendo Pong");
            await SendAsync("Pong", DateTime.UtcNow);
        });
    }

    // ── Procesamiento de eventos de monitoreo ─────────────────────
    private async Task ProcesarEventoAsync(MonitorEvent monitorEvent) {
        try {
            switch(monitorEvent.Tipo) {
                case "ALERTA":
                    _logger.LogWarning("Alerta recibida: {Descripcion}", monitorEvent.Descripcion);
                    // tu lógica aquí
                    await NotificarEstadoAsync("alerta_procesada");
                    break;

                case "REPORTE":
                    _logger.LogInformation("Solicitud de reporte: {Descripcion}", monitorEvent.Descripcion);
                    // tu lógica aquí — ej: disparar el job de eventos
                    await NotificarEstadoAsync("reporte_generado");
                    break;

                default:
                    _logger.LogWarning("Tipo de evento desconocido: {Tipo}", monitorEvent.Tipo);
                    break;
            }
        } catch(Exception ex) {
            _logger.LogError(ex, "Error procesando evento: {Tipo}", monitorEvent.Tipo);
            await NotificarErrorAsync(ex.Message);
        }
    }

    // ── Procesamiento de comandos ─────────────────────────────────
    private async Task ProcesarComandoAsync(string command) {
        try {
            switch(command.ToUpperInvariant()) {
                case "REINICIAR":
                    _logger.LogInformation("Ejecutando comando REINICIAR");
                    // tu lógica aquí
                    break;

                case "STATUS":
                    await SendAsync("StatusResponse", new { Estado = "online", Hora = DateTime.Now });
                    break;

                default:
                    _logger.LogWarning("Comando desconocido: {Command}", command);
                    break;
            }
        } catch(Exception ex) {
            _logger.LogError(ex, "Error procesando comando: {Command}", command);
            await NotificarErrorAsync(ex.Message);
        }
    }

    // ── Enviar mensajes al Hub ────────────────────────────────────
    private async Task SendAsync(string method, object payload = null) {
        if(_connection?.State != HubConnectionState.Connected) {
            _logger.LogWarning("No se pudo enviar '{Method}': conexión no activa", method);
            return;
        }

        try {
            await _connection.SendAsync(method, payload);
            _logger.LogDebug("Mensaje enviado: {Method}", method);
        } catch(Exception ex) {
            _logger.LogError(ex, "Error enviando mensaje '{Method}'", method);
        }
    }

    private Task NotificarEstadoAsync(string estado) =>
        SendAsync("NotificarEstado", new { Estado = estado, Hora = DateTime.UtcNow });

    private Task NotificarErrorAsync(string mensaje) =>
        SendAsync("NotificarError", new { Error = mensaje, Hora = DateTime.UtcNow });

    // ── Conexión con backoff manual ───────────────────────────────
    private CancellationTokenSource _cts = new();

    private async Task ConnectWithBackoffAsync(CancellationToken cancellationToken) {
        int attempt = 0;

        while(!cancellationToken.IsCancellationRequested) {
            try {
                await _connection!.StartAsync(cancellationToken);
                _logger.LogInformation("Conexión iniciada correctamente");

                await NotificarEstadoAsync("online");

                // Mantener el worker vivo mientras la conexión esté activa
                await Task.Delay(Timeout.Infinite, cancellationToken);
            } catch(OperationCanceledException) {
                _logger.LogInformation("Worker detenido por cancellation token");
                break;
            } catch(Exception ex) {
                var delay = attempt < RetryDelays.Length
                    ? RetryDelays[attempt]
                    : RetryDelays[^1]; // máximo: 60s

                _logger.LogError(ex, "Intento {Attempt} fallido. Reintentando en {Delay}s", attempt + 1, delay.TotalSeconds);

                attempt++;
                await Task.Delay(delay, cancellationToken);
            }
        }
    }

    // ── Detener el worker ─────────────────────────────────────────
    public override async Task StopAsync(CancellationToken cancellationToken) {
        _logger.LogInformation("Deteniendo worker...");

        _cts.Cancel();

        if(_connection != null) {
            await NotificarEstadoAsync("offline");
            await _connection.StopAsync(cancellationToken);
            await _connection.DisposeAsync();
        }

        await base.StopAsync(cancellationToken);
    }
}

public class MonitorEvent {
    public string Tipo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public Dictionary<string, object> Metadata { get; set; }
}
