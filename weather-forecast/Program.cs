using weather_forecast;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddLogging();

// Configure OpenTelemetry
builder.ConfigureOpenTelemetry(builder.Configuration.GetSection(OpenTelemetryServiceConfig.Section));

// Register Swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Use(async (context, next) =>
    {
        if (context.Request.Path == "/")
        {
            context.Response.Redirect("/swagger/index.html");
            return;
        }
        await next();
    });
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

// Error handling middleware
app.UseExceptionHandler("/error");

app.Run();

