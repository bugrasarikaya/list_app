using FluentAssertions;
using FluentValidation.Results;
using list_api.Models.DTOs;
using list_api.Models.Validators;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.ValidatorTests {
	public class UserPatchDTOValidatorTests : IClassFixture<RepositoryTestFixture> {
		[Theory]
		[InlineData(-1, "ninesrodriguez", "password")]
		[InlineData(1, "QpYDDXfO4A5jHYmeBYb3rbFBSddxSos2eeRzBBpo8nENlENXU74WOZsiOVd1vUYgCK7y1pHCR7VWM9BiO24yY0q6FAZNo2ku20oHH", "password")]
		[InlineData(1, "ninesrodriguez", "QpYDDXfO4A5jHYmeBYb3rbFBSddxSos2eeRzBBpo8nENlENXU74WOZsiOVd1vUYgCK7y1pHCR7VWM9BiO24yY0q6FAZNo2ku20oHH")]
		public void Give_Invalid_Input_ShouldReturnError(int id_role, string name, string password) {
			ValidationResult dto_validation_result = new UserPatchDTOValidator().Validate(new UserPatchDTO { IDRole = id_role, Name = name, Password = password });
			dto_validation_result.Errors.Count.Should().BeGreaterThan(0);
		}
		[Theory]
		[InlineData(0, "", "")]
		[InlineData(1, "ninesrodriguez", "password")]
		public void Give_Valid_Input_ShouldNotReturnError(int id_role, string name, string password) {
			ValidationResult dto_validation_result = new UserPatchDTOValidator().Validate(new UserPatchDTO { IDRole = id_role, Name = name, Password = password });
			dto_validation_result.Errors.Count.Should().Be(0);
		}
	}
}