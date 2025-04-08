using Serilog;
using Microsoft.EntityFrameworkCore;
using DWPersistence.DataBaseContext;
using MySqlPersistence.DataBaseContext;
using MySqlPersistence;
using Application;
using DWPersistence;
var builder = WebApplication.CreateBuilder(args);
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
        ServerVersion.Parse("8.0.32-mysql")        
    );
});
builder.Services.AddDbContext<DataWarehouseContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataWarehouseConnection"), options => options.EnableRetryOnFailure());
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
