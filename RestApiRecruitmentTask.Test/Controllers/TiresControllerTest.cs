using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using RestApiRecruitmentTask.Core.Models;
using System.Text;

namespace RestApiRecruitmentTask.Test.Controllers
{
    public class TiresControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public TiresControllerTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetTires_ReturnsOkResult_WithListOfTires()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/tires");

            response.EnsureSuccessStatusCode(); // 200 - 299
            var responseBody = await response.Content.ReadAsStringAsync();

            var tires = JsonConvert.DeserializeObject<IEnumerable<Tire>>(responseBody);
            Assert.NotEmpty(tires!);
        }

        [Fact]
        public async Task GetTires_ReturnsNotFound_WhenNoTireExist()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/tires");

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
