using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GameStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);

builder.Services.AddValidation();
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.RequestMethod |
                            HttpLoggingFields.RequestPath |
                            HttpLoggingFields.ResponseStatusCode |
                            HttpLoggingFields.Duration;
    options.CombineLogs = true;
});

WebApplication app = builder.Build();

app.MapGet("/", () => "Welcome to the Game Store API!");

app.MapGames();
app.MapGenres();

app.UseHttpLogging();
//app.UseMiddleware<RequestTimingMiddleware>();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDbAsync();
}

await app.RunAsync();
