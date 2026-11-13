using Serilog;
using Valvenok.Api.Exceptions;
using Valvenok.Infrastructure;
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();
try
{
    Log.Information("Starting server.");
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddSerilog((services, lc) => lc.ReadFrom.Configuration(builder.Configuration).ReadFrom.Services(services));
    builder.Services.AddControllers();
    builder.Services.AddOpenApi();
    builder.Services.ConfigureDatabases(builder.Configuration);
    builder.Services.ConfigureAppHealthChecks();
    builder.Services.ConfigureAzure(builder.Configuration);
    builder.Services.ConfigureInfraServices();
    builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();
    var app = builder.Build();
    app.UseExceptionHandler();
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }
    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.MapReverseProxy();
    app.MapHealthChecks("/health");
    Log.Information("Server started and ready.");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Server terminated unexpectedly.");
}
finally
{
    Log.CloseAndFlush();
}
