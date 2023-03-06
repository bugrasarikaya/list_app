using Newtonsoft.Json;
using System.Net;
using System.Text;
using list_api.Models.DTOs;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.ControllerTests.BrandControllerTests {
	public class GetTests : IClassFixture<ControllerTestFixture> {
		private readonly HttpClient client;
		public GetTests(ControllerTestFixture test_fixture) {
			client = test_fixture.Client;
		}
		[Theory]
		[InlineData("1")]
		[InlineData("Alfa")]
		public async Task Post_DTO_ShouldReturn_Created(string param) {
			HttpResponseMessage response = await client.GetAsync("/api/brand/" + param);
			//Assert.Equal(string.Empty, await response.Content.ReadAsStringAsync());
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}
	}
}