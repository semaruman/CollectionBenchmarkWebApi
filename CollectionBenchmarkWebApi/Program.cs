using CollectionBenchmarkWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ICollesctionsService, CollectionsService>();

var app = builder.Build();
app.MapControllers();
app.MapGet("/", () => "Сравнение производительности коллекций в C#!");

app.Run();
