using CollectionBenchmarkWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<ICollesctionsService, CollectionsService>();

var app = builder.Build();
app.MapControllers();
app.MapGet("/", () => "Сравнение производительности коллекций в C#!");

app.Run();
