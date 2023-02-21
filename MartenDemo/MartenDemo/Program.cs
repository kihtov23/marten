using Marten;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMarten(o=>
    o.Connection(builder.Configuration.GetConnectionString("Default"))
);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/create", async (IDocumentSession session) =>
{
    session.Store(new User("Foo", 20));
    await session.SaveChangesAsync();
    return "created!";
});

app.MapGet("/{guid}", (Guid guid, IQuerySession session) =>
{
    return session.LoadAsync<User>(guid);
});

app.Run();

public record User(string Name, int Age)
{
    public Guid Id { get; set; }
}
