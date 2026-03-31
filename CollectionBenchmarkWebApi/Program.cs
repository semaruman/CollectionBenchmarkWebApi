using CollectionBenchmarkWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<ICollesctionsService, CollectionsService>();

var app = builder.Build();
app.MapControllers();
app.MapGet("/", () => GetMenu);

app.Run();

IResult GetMenu()
{
    return Results.Ok(new
    {
        Endpoints = new[]
        {
            "GET: api/benchmark/types - получить коллекции, которые можно добавить в сравнение",
            "POST: api/benchmark/addcollection - добавить коллекцию в сравнение",
            "POST: api/benchmark/count?count={число элементов} - количество элементов в каждой коллекции",
            "GET: api/benchmark/selectedtypes - получить коллекции, которые уже добавлены в сравнение",
            "GET: api/benchmark/add - сравнить выбранные коллекции по добавлению элементов",
            "GET: api/benchmark/search - сравнить выбранные коллекции поиску элемента",
            "GET: api/benchmark/memory - сравнить выбранные коллекции по потреблении элементов",
        }
    });
}