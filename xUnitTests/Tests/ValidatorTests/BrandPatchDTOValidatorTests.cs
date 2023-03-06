using FluentAssertions;
using FluentValidation.Results;
using list_api.Models.DTOs;
using list_api.Models.Validators;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.ValidatorTests {
	public class BrandPatchDTOValidatorTests : IClassFixture<RepositoryTestFixture> {
		[Theory]
		[InlineData("QpYDDXfO4A5jHYmeBYb3rbFBSddxSos2eeRzBBpo8nENlENXU74WOZsiOVd1vUYgCK7y1pHCR7VWM9BiO24yY0q6FAZNo2ku20oHH")]
		public void Give_Invalid_Input_ShouldReturnError(string name) {
			ValidationResult dto_validation_result = new BrandPatchDTOValidator().Validate(new BrandPatchDTO { Name = name });
			dto_validation_result.Errors.Count.Should().BeGreaterThan(0);
		}
		[Theory]
		[InlineData("")]
		[InlineData("Dove")]
		public void Give_Valid_Input_ShouldNotReturnError(string name) {
			ValidationResult dto_validation_result = new BrandPatchDTOValidator().Validate(new BrandPatchDTO { Name = name });
			dto_validation_result.Errors.Count.Should().Be(0);
		}
	}
}