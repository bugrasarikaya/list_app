using FluentAssertions;
using list_api.Security;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.ValidatorTests {
	public class SecurityValidatorTests : IClassFixture<RepositoryTestFixture> {
		[Theory]
		[InlineData("")]
		[InlineData("sdfehyaghaeh")]
		public void Give_InValid_DateTimeStringInput_ToStringParameter_ShouldNotReturnError(string param) {
			SecurityValidator security_validator = new SecurityValidator(param);
			security_validator.RefreshTokenValidator().Should().Be(false);
		}
		[Theory]
		[InlineData("8674abd2-a801-4eb2-bcd6-39fed928f06a")]
		public void Give_Valid_Input_ShouldNotReturnError(string param) {
			SecurityValidator security_validator = new SecurityValidator(param);
			security_validator.RefreshTokenValidator().Should().Be(true);
		}
	}
}