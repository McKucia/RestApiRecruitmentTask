using Microsoft.AspNetCore.Mvc.Testing;

namespace RestApiRecruitmentTask.Test.Fixtures
{
    public class ProducerFixture
    {
        public HttpClient Client { get; }
        private readonly WebApplicationFactory<Program> _factory;

        public ProducerFixture()
        {
            _factory = new WebApplicationFactory<Program>();
            Client = _factory.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            _factory.Dispose();
        }
    }
}
