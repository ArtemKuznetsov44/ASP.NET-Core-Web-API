using MetricsManager;
<<<<<<< HEAD
using MetricsManager.Convertors;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NLog.Web; 

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
logger.Debug("init main");

// Блок try-catch для "отлова" возможных ошибок:
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers().AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new CustomTimeSpanConverter()));

    #region SWAGGER
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    // Modified SWAGGER:
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsAgent", Version = "v1" });

        // TimeSpan format supporting:
        c.MapType<TimeSpan>(() => new OpenApiSchema
        {
            Type = "string",
            Example = new OpenApiString("00:00:00")
        });
    });
    #endregion

    #region Configure Logging

    // Adding the main logging service:
    builder.Host.ConfigureLogging(logging =>
    {
        logging.ClearProviders(); // Remove other ILogging providers from builder. 
        logging.AddConsole(); // Add a console logger.

    }).UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = true });

    // Some settings for http-logging-requests|response: 
    builder.Services.AddHttpLogging(logging =>
    {
        logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
        // Size limits: 
        logging.RequestBodyLogLimit = 4096; // For request (input).
        logging.RequestBodyLogLimit = 4096; // For response (output).
        // Headers for logging:
        logging.ResponseHeaders.Add("Authorization");
        logging.ResponseHeaders.Add("X-Real-IP");
        logging.ResponseHeaders.Add("X-Forwarded-For");
    });
    #endregion 

    builder.Services.AddSingleton<AgentsRepository>();
    
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.UseHttpLogging(); // If we whant to add http-logging. 

    app.MapControllers();

    app.Run();
}
// В случае выялвления ошибки:
catch (Exception ex)
{
    // Логирование ошибки и пояснения:
    logger.Error(ex.Message, "Stopped program because of exception");
    throw; 
}
finally
{
    NLog.LogManager.Shutdown(); 
}

=======

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<AgentsRepository>(); 

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
>>>>>>> 82c1144b80e48698a5950e4a80dd8d4719588fbc
