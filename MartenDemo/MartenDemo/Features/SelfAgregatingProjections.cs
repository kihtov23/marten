using Marten;

namespace MartenDemo.Features
{
    public class SelfAgregatingProjections
    {
        public static void TryThis(WebApplication app)
        {
            app.MapGet("/user", async (IDocumentSession session) =>
            {
                var someGuid = Guid.NewGuid();

                session.Events.StartStream(someGuid, new UpdateEmail("test@gmail.com"));
                await session.SaveChangesAsync();

                session.Events.Append(someGuid, new UpdateEmail("newEmail@gmail.com"));
                session.Events.Append(someGuid, new UpdatedName("SomeName"));
                session.Events.Append(someGuid, new UpdatedAge(20));
                await session.SaveChangesAsync();

                var user = await session.Events.AggregateStreamAsync<User>(someGuid);
                return user;
            });
        }
    }

    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public void Apply(UpdateEmail email)
        {
            Email = email.Email;
        }
        public void Apply(UpdatedName name)
        {
            Name = name.Name;
        }

        public void Apply(UpdatedAge age)
        {
            Age = age.Age; 
        }

    }

    public record UpdateEmail(string Email);
    public record UpdatedName(string Name);
    public record UpdatedAge(int Age);
}
