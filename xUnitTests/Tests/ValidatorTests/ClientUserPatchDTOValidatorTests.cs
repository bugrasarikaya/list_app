using FluentAssertions;
using FluentValidation.Results;
using list_api.Models.DTOs;
using list_api.Models.Validators;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.ValidatorTests {
	public class ClientUserPatchDTOValidatorTests : IClassFixture<RepositoryTestFixture> {
		[Theory]
		[InlineData("8cI3dFz", "password")]
		[InlineData("QpYDDXfO4A5jHYmeBYb3rbFBSddxSos2eeRzBBpo8nENlENXU74WOZsiOVd1vUYgCK7y1pHCR7VWM9BiO24yY0q6FAZNo2ku20oHH", "password")]
		[InlineData("ninesrodriguez", "8cI3dFz")]
		[InlineData("ninesrodriguez", "QpYDDXfO4A5jHYmeBYb3rbFBSddxSos2eeRzBBpo8nENlENXU74WOZsiOVd1vUYgCK7y1pHCR7VWM9BiO24yY0q6FAZNo2ku20oHH")]
		public void Give_Invalid_Input_ShouldReturnError(string name, string password) {
			ValidationResult dto_validation_result = new ClientUserPatchDTOValidator().Validate(new ClientUserPatchDTO { Name = name, Password = password });
			dto_validation_result.Errors.Count.Should().BeGreaterThan(0);
		}
		[Theory]
		[InlineData("", "")]
		[InlineData("ninesrodriguez", "password")]
		public void Give_Valid_Input_ShouldNotReturnError(string name, string password) {
			ValidationResult dto_validation_result = new ClientUserPatchDTOValidator().Validate(new ClientUserPatchDTO { Name = name, Password = password });
			dto_validation_result.Errors.Count.Should().Be(0);
		}
	}
}