using CollectionBenchmarkWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//сервисы для сваггер
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //добавляю генерацию JSON и UI
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapGet("/", GetMenu);

app.Run();

IResult GetMenu()
{
    return Results.Ok(new
    {
        Endpoints = new[]
        {
            "GET: api/benchmark/types - получить коллекции, которые можно добавить в сравнение",
            "POST: api/benchmark/addcollection?type={тип коллекции} - добавить коллекцию в сравнение",
            "POST: api/benchmark/count?count={число элементов} - количество элементов в каждой коллекции",
            "GET: api/benchmark/selectedtypes - получить коллекции, которые уже добавлены в сравнение",
            "GET: api/benchmark/add - сравнить выбранные коллекции по добавлению элементов",
            "GET: api/benchmark/search - сравнить выбранные коллекции поиску элемента",
            "GET: api/benchmark/memory - сравнить выбранные коллекции по потреблении элементов",
            "GET: api/benchmark/remove - сравнить выбранные коллекции по удалению элементов",
        }
    });
}