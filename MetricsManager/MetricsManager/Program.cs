using MetricsManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<AgentsRepository>(); 

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
