using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace weather_forecast;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
{
    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
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

        logger.LogInformation("Generated forecast: {forecast}", JsonSerializer.Serialize(forecast));
        
        return forecast;
    }
}

public class WeatherForecast
{
    public DateTime Date { get; init; }
    public int Temperature { get; init; }
    public string Summary { get; init; } = null!;
}