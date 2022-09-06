using NLog;
using NLog.Web;

var logger = LogManager
    .Setup()
    .LoadConfigurationFromFile("nlog.config")
    .GetCurrentClassLogger();

logger.Info("Application starting");

try
{
    var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

    builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Application stopped because of an unhandled exception");
}
finally
{
    logger.Info("Application stopped");
    LogManager.Shutdown();
}