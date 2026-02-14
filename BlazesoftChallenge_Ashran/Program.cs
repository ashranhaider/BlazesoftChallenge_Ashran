using BlazesoftChallenge_Ashran.Data;
using BlazesoftChallenge_Ashran.Middleware;
using BlazesoftChallenge_Ashran.Repositories;
using BlazesoftChallenge_Ashran.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<MongoContext>();
builder.Services.AddScoped<IGameConfigRepository, GameConfigRepository>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddSingleton<IWinCalculator, WinCalculator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "App is running");
app.Run();
