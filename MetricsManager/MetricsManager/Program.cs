using AutoMapper;
using FluentMigrator.Runner;
using MetricsManager;
using MetricsManager.Convertors;
using MetricsManager.Models;
using MetricsManager.Services;
using MetricsManager.Services.Implimintation;
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

    #region Configure AutoMapper:

    // Create a configuration object which is based on MapperProfile class:
    var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile())); // Config.
    var mapper = mapperConfiguration.CreateMapper(); // Create mapper from our configuration for mapper.
    // Add this object like a Singleton object for our app:
    builder.Services.AddSingleton(mapper);

    #endregion
    #region Configure json-converter:

    builder.Services.AddControllers().AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new CustomTimeSpanConverter()));

    #endregion

    #region Configure Swagger:
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    // Modified SWAGGER:
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsManager", Version = "v1" });

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

    #region Configure Options:

    builder.Services.Configure<DataBaseOptions>(options =>
    {
        builder.Configuration.GetSection("Settings:DataBaseOptions").Bind(options);
    });

    #endregion

    #region Configure DataBase:

    builder.Services.AddFluentMigratorCore()
        .ConfigureRunner(rb =>
        rb.AddSQLite()
        .WithGlobalConnectionString(builder.Configuration["Settings:DataBaseOptions:ConnectionString"].ToString()) // connectionString
        .ScanIn(typeof(Program).Assembly).For.Migrations() // Proccess of finding our migrations
        ).AddLogging(lb => lb.AddFluentMigratorConsole()); // For cnosole logging migrations. 

    #endregion

    #region Configure Repositories:

    builder.Services.AddSingleton<IAgentsRepository, AgentsRepository>(); 

    #endregion

    builder.Services.AddHttpClient(); 
    
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

    // IScopeFactory can create a scope element or service.
    var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
    using (IServiceScope serviceScope = serviceScopeFactory.CreateScope())
    {
        var migrationRunner = serviceScope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        migrationRunner.MigrateUp(); // Apply migration to the last migration version. 
        // migrationRunner.MigrateUp(1); // Apply migration to the specified version.
        // migrationRunner.MigrateDown(0); 
    }

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

