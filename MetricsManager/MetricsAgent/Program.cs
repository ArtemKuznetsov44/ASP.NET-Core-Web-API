using AutoMapper;
using MetricsAgent;
using MetricsAgent.Convertors;
using MetricsAgent.Models;
using MetricsAgent.Services;
using MetricsAgent.Services.Implimintation;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NLog.Web;
using System.Data.SQLite;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
logger.Debug("init main");

#region Configure SQL Lite connection & prepearing schemes
static void ConfigureSqlLiteConnection(WebApplicationBuilder? builder)
{
    string connectionString = builder.Configuration["Settings:DataBaseOptions:ConnectionString"].ToString();

    var connection = new SQLiteConnection(connectionString);
    connection.Open();
    PrepareSchema(connection);
}

static void PrepareSchema(SQLiteConnection connection)
{
    using (var command = new SQLiteCommand(connection))
    {
        #region cpumetrics table:
        // ������ ����� ����� �������� ��� ����������.
        // ������� ������� � ���������, ���� ��� ���� � ���� ������.
        command.CommandText = "DROP TABLE IF EXISTS cpumetrics;";
        // ���������� ������ � ���� ������
        command.ExecuteNonQuery(); // ���������� ��������� ����� ������.
        // ����� ����� �������: ������������ �������, � ������� ��������� ���� - id ���� ������ int,
        // value - �������� �������,
        // time - �����, ����� ���� ������� �������.
        // ����: ����� ������� �� ���� ��������.
        command.CommandText =
            @"CREATE TABLE cpumetrics(
            id INTEGER PRIMARY KEY,
            value INT, time INT)";
        command.ExecuteNonQuery(); // ���������� ��������� ����� ������.
        #endregion

        #region dotnetmetrics table:

        command.CommandText = "DROP TABLE IF EXISTS dotnetmetrics;";
        command.ExecuteNonQuery();
        command.CommandText = @"CREATE TABLE dotnetmetrics(
            id INTEGER PRIMARY KEY,
            value INT, time INT)";
        command.ExecuteNonQuery();

        #endregion

        #region hddmetrics table:

        command.CommandText = "DROP TABLE IF EXISTS hddmetrics;";
        command.ExecuteNonQuery();
        command.CommandText = @"CREATE TABLE hddmetrics(
            id INTEGER PRIMARY KEY,
            value INT, time INT)";
        command.ExecuteNonQuery();

        #endregion

        #region networkmetrics table:

        command.CommandText = "DROP TABLE IF EXISTS networkmetrics;";
        command.ExecuteNonQuery();
        command.CommandText = @"CREATE TABLE networkmetrics(
            id INTEGER PRIMARY KEY,
            value INT, time INT)";
        command.ExecuteNonQuery();

        #endregion

        #region rammetrics table:

        command.CommandText = "DROP TABLE IF EXISTS rammetrics;";
        command.ExecuteNonQuery();
        command.CommandText = @"CREATE TABLE rammetrics(
            id INTEGER PRIMARY KEY,
            value INT, time INT)";
        command.ExecuteNonQuery();

        #endregion
    }
}
#endregion 

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

    #region Configure Options:

    builder.Services.Configure<DataBaseOptions>(options =>
    {
        builder.Configuration.GetSection("Settings:DataBaseOptions").Bind(options);
    }); 

    #endregion

    #region Controllers & JsonOptions:
    // Adding custom json-serializer as option.
    builder.Services.AddControllers().AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new CustomTimeSpanConverter()));

    #endregion

    #region Configure Repositories:

    builder.Services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
    builder.Services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>();
    builder.Services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
    builder.Services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
    builder.Services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();

    #endregion

    #region Configure DataBase:
    // Call method for config our SQL lite dataBase-connection
    ConfigureSqlLiteConnection(builder);

    #endregion

    #region Configure Swagger:
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

    #region Configure Logging:

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
// In case with exceptions:
catch (Exception ex)
{
    logger.Error(ex.Message, "Stopped program because of exception");
    throw;
}
finally { NLog.LogManager.Shutdown(); }



