using Newtonsoft.Json;
using RestApiRecruitmentTask.Api.ViewModels;
using RestApiRecruitmentTask.Core.Models;
using RestApiRecruitmentTask.Test.Fixtures;
using System.Text;

namespace RestApiRecruitmentTask.Test.Controllers
{
    public class ProducersControllerTests : IClassFixture<ProducerFixture>
    {
        private readonly HttpClient _client;

        public ProducersControllerTests(ProducerFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task GetProducers_ReturnsOkResult_WhenProducerIsCreated()
        {
            var producer = new Producer
            {
                Name = "michelin",
                Class = "premium"
            };
            var content = new StringContent(JsonConvert.SerializeObject(producer), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/producers", content);

            var response = await _client.GetAsync("/api/producers");

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var producers = JsonConvert.DeserializeObject<IEnumerable<Producer>>(responseBody);
            Assert.NotEmpty(producers); 
        }

        [Fact]
        public async Task CreateProducer_ReturnsCreated_WhenProducerIsValid()
        {
            var producer = new Producer
            {
                Name = "pirelli",
                Class = "casual"
            };
            var content = new StringContent(JsonConvert.SerializeObject(producer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/producers", content);

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task CreateProducer_ReturnsBadRequest_WhenProducerModelIsInvalid()
        {
            var producer = new Producer
            {
                Name = "",
                Class = "Budget"
            };
            var content = new StringContent(JsonConvert.SerializeObject(producer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/producers", content);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateProducer_ReturnsOkResult_And_ProducerIsUpdated()
        {
            var producer = new ProducerViewModel
            {
                Name = "goodyear",
                Class = "premium"
            };

            var content = new StringContent(JsonConvert.SerializeObject(producer), Encoding.UTF8, "application/json");
            var createResponse = await _client.PostAsync("/api/producers", content);
            createResponse.EnsureSuccessStatusCode();

            var createdProducer = JsonConvert.DeserializeObject<Producer>(await createResponse.Content.ReadAsStringAsync());

            Assert.NotNull(createdProducer);
            Assert.Equal("goodyear", createdProducer.Name);

            var producerId = createdProducer.Id;

            var updatedProducer = new ProducerViewModel
            {
                Name = "michelin",
                Class = "premium"
            };

            var updateContent = new StringContent(JsonConvert.SerializeObject(updatedProducer), Encoding.UTF8, "application/json");

            var updateResponse = await _client.PutAsync($"/api/producers/{producerId}", updateContent);

            updateResponse.EnsureSuccessStatusCode();

            var getResponse = await _client.GetAsync($"/api/producers/{producerId}");

            getResponse.EnsureSuccessStatusCode();
            var updatedResponseBody = await getResponse.Content.ReadAsStringAsync();
            var updatedProducerFromApi = JsonConvert.DeserializeObject<Producer>(updatedResponseBody);

            Assert.NotNull(updatedProducerFromApi);
            Assert.Equal("michelin", updatedProducerFromApi?.Name);
        }


        [Fact]
        public async Task DeleteProducer_ReturnsNoContent_WhenProducerIsDeleted()
        {
            var producer = new Producer
            {
                Name = "goodyear",
                Class = "mid"
            };
            var content = new StringContent(JsonConvert.SerializeObject(producer), Encoding.UTF8, "application/json");
            var createResponse = await _client.PostAsync("/api/producers", content);

            var createdProducer = JsonConvert.DeserializeObject<Producer>(await createResponse.Content.ReadAsStringAsync());
            var producerId = createdProducer.Id;

            var deleteResponse = await _client.DeleteAsync($"/api/producers/{producerId}");

            Assert.Equal(System.Net.HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }
    }
}
