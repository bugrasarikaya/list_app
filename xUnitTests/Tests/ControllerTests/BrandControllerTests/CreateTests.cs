using Newtonsoft.Json;
using System.Net;
using System.Text;
using list_api.Models.DTOs;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.ControllerTests.BrandControllerTests {
	public class CreateTests : IClassFixture<ControllerTestFixture> {
		private readonly HttpClient client;
		public CreateTests(ControllerTestFixture test_fixture) {
			client = test_fixture.Client;
		}
		[Fact]
		public async Task Post_Brand_ShouldReturn_Created() {
			HttpResponseMessage response = await client.PostAsync("/api/brand/", new StringContent(JsonConvert.SerializeObject(new BrandDTO { Name = "Dove" }), Encoding.UTF8, "application/json"));
			//Assert.Equal(string.Empty, await response.Content.ReadAsStringAsync());
			Assert.Equal(HttpStatusCode.Created, response.StatusCode);
		}
	}
}