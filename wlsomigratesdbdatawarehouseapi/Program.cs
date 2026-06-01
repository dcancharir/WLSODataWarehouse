using Serilog;
using Microsoft.EntityFrameworkCore;
using DWPersistence.DataBaseContext;
using MySqlPersistence.DataBaseContext;
using MySqlPersistence;
using Application;
using DWPersistence;
using Quartz;
using wlsomigratesdbdatawarehouseapi.Jobs;
var builder = WebApplication.CreateBuilder(args);

var mySqlVersion = builder.Configuration.GetValue<string>("Variables:MySqlVersion")?? "8.0.32-mysql";

string HoraMigracion = builder.Configuration.GetValue<string>("Jobs:HoraMigracion") ?? "08:00";
var realizarMigracion = builder.Configuration.GetValue<bool>("Jobs:RealizarMigracion");

var RealizarMigracionConstante = builder.Configuration.GetValue<bool>("Jobs:RealizarMigracionConstante");
string HoraMigracionConstante = builder.Configuration.GetValue<string>("Jobs:HoraMigracionConstante") ?? "08:00";

bool RealizarCreacionFechas = builder.Configuration.GetValue<bool>("Jobs:RealizarCreacionFechas");
string HoraCreacionFechas = builder.Configuration.GetValue<string>("Jobs:HoraCreacionFechas") ?? "01:00";

bool RealizarMigracionRealGameEvents = builder.Configuration.GetValue<bool>("Jobs:RealizarMigracionRealGameEvents");
string HoraMigracionRealGameEvents = builder.Configuration.GetValue<string>("Jobs:HoraMigracionRealGameEvents") ??"08:00";

IConfiguration configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();
// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddMysqlPersistence();
builder.Services.AddDWPersistence();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

/**/
builder.WebHost.ConfigureKestrel(serverOptions => {
    serverOptions.Limits.MaxRequestBodySize = long.MaxValue;
});
builder.Host.ConfigureLogging(logging => {
    var logFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
    if(!Directory.Exists(logFolder)) {
        Directory.CreateDirectory(logFolder);
    }

    var logFile = Path.Combine(logFolder, "wlsomigratesdb-.log");
    logging.AddSerilog();
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Error)
        .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Error)
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Error)
        .WriteTo.File(logFile, rollingInterval: RollingInterval.Day)
        .CreateLogger();
});
ILogger<Program> logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();
builder.Services.AddDbContext<MySqlContext>(options => {
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySqlConnection"),
        ServerVersion.Parse(mySqlVersion),
        opt => {
            opt.CommandTimeout(300);
            opt.EnableRetryOnFailure();
        }
    );
});
builder.Services.AddDbContext<DataWarehouseContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataWarehouseConnection"), options => options.EnableRetryOnFailure());
});
builder.Services.AddQuartz(q => {
    if(realizarMigracion) {
        JobKey key = new JobKey("MigracionDiariaJob");
        q.AddJob<MigracionWSLOJob>(jobConfig => jobConfig.WithIdentity(key));
        var hora = Convert.ToDateTime(HoraMigracion);
        q.AddTrigger(opts => opts
                .ForJob(key)
                .WithIdentity("MigracionDiariaJob-trigger")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(hora.Hour, hora.Minute))
                //.WithCronSchedule(CronMigracionDiaria)
                //.WithSimpleSchedule(x => x
                //    .WithIntervalInHours(intervaloHorasMigracion)
                //    .RepeatForever().Build())
                .StartNow()
        );
    }
    if(RealizarMigracionConstante) {
        JobKey key = new JobKey("MigracionConstanteJob");
        var hora = Convert.ToDateTime(HoraMigracionConstante);
        q.AddJob<MigracionConstanteJob>(jobConfig => jobConfig.WithIdentity(key));
        q.AddTrigger(opts => opts
                .ForJob(key)
                .WithIdentity("MigracionConstanteJob-trigger")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(hora.Hour, hora.Minute))
                //.WithSimpleSchedule(x => x
                //    .WithIntervalInMinutes(IntervaloMinutosMigracionConstante)
                //    .RepeatForever().Build())
                .StartNow()
        );
    }
    if(RealizarCreacionFechas) {
        JobKey key = new JobKey("CreateTablaHistorialJob");
        var hora = Convert.ToDateTime(HoraCreacionFechas);
        q.AddJob<TablaHistorialJob>(jobConfig => jobConfig.WithIdentity(key));
        q.AddTrigger(opts => opts
                .ForJob(key)
                .WithIdentity("CreateTablaHistorialJob-trigger")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(hora.Hour, hora.Minute))
                //.WithSimpleSchedule(x => x
                //    .WithIntervalInHours(IntervaloHorasCreacionFechas)
                //    .RepeatForever().Build())
                .StartNow()
                );
    }
    if(RealizarMigracionRealGameEvents) {
        JobKey key = new JobKey("MigrarRealGameEventsJob");
        var hora = Convert.ToDateTime(HoraMigracionRealGameEvents);
        q.AddJob<MigrarRealGameEventsJob>(jobConfig => jobConfig.WithIdentity(key));
        q.AddTrigger(opts => opts
                .ForJob(key)
                .WithIdentity("MigrarRealGameEventsJob-trigger")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(hora.Hour, hora.Minute))
                //.WithSimpleSchedule(x => x
                //    .WithIntervalInHours(IntervaloMinutosMigrarRealGameEvents)
                //    .RepeatForever().Build())
                .StartNow()
                );
    }
}).AddQuartzHostedService(options => {
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
});
/**/
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Lifetime.ApplicationStarted.Register(() => {
    logger.LogInformation("La aplicacion se ha iniciado");
    Console.WriteLine($"Contexto DataWarehouse {builder.Configuration.GetConnectionString("DataWarehouseConnection")}");
    Console.WriteLine($"Contexto MySql{builder.Configuration.GetConnectionString("MySqlConnection")}");

});
app.Lifetime.ApplicationStopping.Register(() => Console.WriteLine("La aplicación está deteniéndose."));
app.Lifetime.ApplicationStopped.Register(() => Console.WriteLine("La aplicación ha sido detenida."));

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
