using Marten;
using MartenDemo.Features;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMarten(o=>
    o.Connection(builder.Configuration.GetConnectionString("Default"))
);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");


//CreateAndGet.TryThis(app);
SelfAgregatingProjections.TryThis(app);

app.Run();





