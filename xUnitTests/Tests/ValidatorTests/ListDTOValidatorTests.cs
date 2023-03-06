using FluentAssertions;
using FluentValidation.Results;
using list_api.Models.DTOs;
using list_api.Models.Validators;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.ValidatorTests {
	public class ListDTOValidatorTests : IClassFixture<RepositoryTestFixture> {
		[Theory]
		[InlineData(0, 1, "My List", "My best list.")]
		[InlineData(1, 0, "My List", "My best list.")]
		[InlineData(1, 1, "", "My best list.")]
		[InlineData(1, 1, "QpYDDXfO4A5jHYmeBYb3rbFBSddxSos2eeRzBBpo8nENlENXU74WOZsiOVd1vUYgCK7y1pHCR7VWM9BiO24yY0q6FAZNo2ku20oHH", "My best list.")]
		[InlineData(1, 1, "My List", "S1GjDxwDvDV3SnnBsRmxV4Hkgi8rKuyRoQfeb8CdIoicz8IrGQVWVeEc5VOh3qlRl9ha2O2XZJ9TgM84AObXlANhCIKEukBjI6Z3flKASMbP1RLt4HZJZlWRhvY0MnM39ETpJXRY5DwwjQnhzVuhxyEQXVJrBHvdwEhgC2exAWzGSLwzwRo2eKLiQBnyEOUjhpN46vacV")]
		public void Give_Invalid_Input_ShouldReturnError(int id_category, int id_user, string name, string description) {
			ValidationResult dto_validation_result = new ListDTOValidator().Validate(new ListDTO { IDCategory = id_category, IDUser = id_user, Name = name, Description = description });
			dto_validation_result.Errors.Count.Should().BeGreaterThan(0);
		}
		[Theory]
		[InlineData(1, 1, "My List", "")]
		[InlineData(1, 1, "My List", "My best list.")]
		public void Give_Valid_Input_ShouldNotReturnError(int id_category, int id_user, string name, string description) {
			ValidationResult dto_validation_result = new ListDTOValidator().Validate(new ListDTO { IDCategory = id_category, IDUser = id_user, Name = name, Description = description });
			dto_validation_result.Errors.Count.Should().Be(0);
		}
	}
}