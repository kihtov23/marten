using Marten;

namespace TestMartenFeatures
{
    public class UnitTest1
    {
        public UnitTest1()
        {
            var documentStore = DocumentStore.For(options =>
            {
                options.Connection("host=127.0.0.1;database=marten-demo;user id=postgres;password=MyPass@word");
            });
        }

        [Fact]
        public void Test1()
        {

        }
    }
}