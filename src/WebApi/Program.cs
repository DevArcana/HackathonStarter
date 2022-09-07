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

    builder.Services.AddControllers();
    builder.Services.AddAuthentication().AddCookie();
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();
    
    app.UseAuthentication();
    app.UseAuthorization();
    
    app.MapControllers();
    app.MapFallbackToFile("index.html");
    
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