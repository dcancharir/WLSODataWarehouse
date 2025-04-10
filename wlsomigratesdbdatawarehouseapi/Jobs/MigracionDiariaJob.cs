using Application.CommandsQueries.AssociateCQ;
using Application.CommandsQueries.BrandCQ;
using Application.CommandsQueries.CustomerCQ;
using Application.CommandsQueries.CustomersGroupCQ;
using Application.CommandsQueries.GameCQ;
using Application.CommandsQueries.GroupsxCQ;
using Application.CommandsQueries.PaymentMethodCQ;
using Application.CommandsQueries.PlayerCQ;
using Application.CommandsQueries.ProcessorCQ;
using Application.CommandsQueries.ProviderCQ;
using Application.CommandsQueries.RealGameEventCQ;
using Application.CommandsQueries.StoreCQ;
using Application.CommandsQueries.StoreTxCQ;
using MediatR;
using Quartz;
using System.Net.NetworkInformation;
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
            _logger.LogInformation($"Job MigracionDiariaJob iniciado");
            _logger.LogInformation($"Metodo AssociateMigration finalizado");
            await AssociateMigration(_mediator);
            _logger.LogInformation($"Metodo BrandMigration finalizado");
            await BrandMigration(_mediator);
            _logger.LogInformation($"Metodo CustomerMigration finalizado");
            await CustomerMigration(_mediator);
            _logger.LogInformation($"Metodo CustomersGroupMigration finalizado");
            await CustomersGroupMigration(_mediator);
            _logger.LogInformation($"Metodo GameMigration finalizado");
            await GameMigration(_mediator);
            _logger.LogInformation($"Metodo GroupsxMigration finalizado");
            await GroupsxMigration(_mediator);
            _logger.LogInformation($"Metodo PaymentMethodMigration finalizado");
            await PaymentMethodMigration(_mediator);
            _logger.LogInformation($"Metodo PlayerMigration finalizado");
            await PlayerMigration(_mediator);
            _logger.LogInformation($"Metodo ProcessorMigration finalizado");
            await ProcessorMigration(_mediator);
            _logger.LogInformation($"Metodo ProviderMigration finalizado");
            await ProviderMigration(_mediator);
            _logger.LogInformation($"Metodo RealGameEventMigration finalizado");
            await RealGameEventMigration(_mediator);
            _logger.LogInformation($"Metodo StoreMigration finalizado");
            await StoreMigration(_mediator);
            _logger.LogInformation($"Metodo StoreTxMigration finalizado");
            await StoreTxMigration(_mediator);
            _logger.LogInformation($"Job MigracionDiariaJob finalizado");
        }
        await Task.CompletedTask;
    }
    internal static async Task AssociateMigration(IMediator _mediator) {
        await _mediator.Send(new MigrarAssociateCommand() { });
    }
    internal static async Task BrandMigration(IMediator _mediator) {
        await _mediator.Send(new MigrarBrandCommand() { });
    }
    internal static async Task CustomerMigration(IMediator _mediator) {
        await _mediator.Send(new MigrarCustomerCommand() { });
    }
    internal static async Task CustomersGroupMigration(IMediator _mediator) {
        await _mediator.Send(new MigrarCustomersGroupCommand() { });
    }
    internal static async Task GameMigration(IMediator _mediator) {
        await _mediator.Send(new MigrarGameCommand() { });
    }
    internal static async Task GroupsxMigration(IMediator _mediator) {
        await _mediator.Send(new MigrarGroupsxCommand() { });
    }
    internal static async Task PaymentMethodMigration(IMediator _mediator) {
        await _mediator.Send(new MigrarPaymentMethodCommand() { });
    }
    internal static async Task PlayerMigration(IMediator _mediator) {
        await _mediator.Send(new MigrarPlayersCommand() { });
    }
    internal static async Task ProcessorMigration(IMediator _mediator) {
        await _mediator.Send(new MigrarProcessorCommand() { });
    }
    internal static async Task ProviderMigration(IMediator _mediator) {
        await _mediator.Send(new MigrarProviderCommand() { });
    }
    internal static async Task RealGameEventMigration(IMediator _mediator) {
        await _mediator.Send(new MigrarRealGameEventCommand() { });
    }
    internal static async Task StoreMigration(IMediator _mediator) {
        await _mediator.Send(new MigrarStoreCommand() { });
    }
    internal static async Task StoreTxMigration(IMediator _mediator) {
        await _mediator.Send(new MigrarStoreTxCommand() { });
    }
}
