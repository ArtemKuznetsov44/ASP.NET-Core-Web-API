using WeatherForecasts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Start singleTone using. 
builder.Services.AddSingleton<Repository>(); 

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
