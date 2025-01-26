using weather_forecast;

var builder = WebApplication.CreateBuilder(args);

// Configure OpenTelemetry
builder.ConfigureOpenTelemetry(builder.Configuration.GetSection(OpenTelemetryServiceConfig.Section));

// Clear default logging providers used by WebApplication host.
builder.Logging.ClearProviders();

// Register Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Get the logger
var logger = app.Services.GetRequiredService<ILogger<Program>>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Error handling middleware
app.UseExceptionHandler("/error");

// Map weather forecast endpoint
app.MapGet("/weatherforecast", () =>
    {
        logger.LogInformation("Processing /weatherforecast endpoint");
        var forecast = Enumerable.Range(1, 5).Select(index =>
        {
            return new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                Temperature = Random.Shared.Next(-20, 55),
                Summary = GetSummary(Random.Shared.Next(0, 5))
            };

            static string GetSummary(int next) =>
                next switch
                {
                    0 => "Freezing",
                    1 => "Bracing",
                    2 => "Chilly",
                    3 => "Cool",
                    4 => "Mild",
                    _ => "Warm"
                };
            
        }).ToArray();

        logger.LogInformation("Generated forecast: {Forecast}", forecast);
        
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.Run();

internal record WeatherForecast
{
    public DateTime Date { get; init; }
    public int Temperature { get; init; }
    public string Summary { get; init; } = null!;
}