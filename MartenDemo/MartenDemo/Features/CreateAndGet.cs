using Marten;

namespace MartenDemo.Features
{
    public class CreateAndGet
    {
        public static void TryThis(IDocumentSession session)
        {

            session.Store(new SimpleUser("Foo", 20));
            session.SaveChangesAsync().GetAwaiter().GetResult();
            var simpleUser = session.LoadAsync<SimpleUser>(guid).GetAwaiter().GetResult();
        }
    }

    public record SimpleUser(string Name, int Age)
    {
        public Guid Id { get; set; }
    }

}
