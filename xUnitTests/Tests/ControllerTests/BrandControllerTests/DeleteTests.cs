using Newtonsoft.Json;
using System.Net;
using System.Text;
using list_api.Models.DTOs;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.ControllerTests.BrandControllerTests {
	public class DeleteTests : IClassFixture<ControllerTestFixture> {
		private readonly HttpClient client;
		public DeleteTests(ControllerTestFixture test_fixture) {
			client = test_fixture.Client;
		}
		[Fact]
		public async Task Delete_Brand_ShouldReturn_NoContent() {
			HttpResponseMessage response = await client.DeleteAsync("/api/brand/1");
			//Assert.Equal(string.Empty, await response.Content.ReadAsStringAsync());
			Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
		}
	}
}