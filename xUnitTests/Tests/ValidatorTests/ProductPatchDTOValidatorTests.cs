using FluentAssertions;
using FluentValidation.Results;
using list_api.Models.DTOs;
using list_api.Models.Validators;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.ValidatorTests {
	public class ProductPatchDTOValidatorTests : IClassFixture<RepositoryTestFixture> {
		[Theory]
		[InlineData(-1, 1, "Gaming PC", "RTX ON", 9999.99)]
		[InlineData(-1, -1, "Gaming PC", "RTX ON", 9999.99)]
		[InlineData(1, 1, "QpYDDXfO4A5jHYmeBYb3rbFBSddxSos2eeRzBBpo8nENlENXU74WOZsiOVd1vUYgCK7y1pHCR7VWM9BiO24yY0q6FAZNo2ku20oHH", "RTX ON", 9999.99)]
		[InlineData(1, 1, "Gaming PC", "S1GjDxwDvDV3SnnBsRmxV4Hkgi8rKuyRoQfeb8CdIoicz8IrGQVWVeEc5VOh3qlRl9ha2O2XZJ9TgM84AObXlANhCIKEukBjI6Z3flKASMbP1RLt4HZJZlWRhvY0MnM39ETpJXRY5DwwjQnhzVuhxyEQXVJrBHvdwEhgC2exAWzGSLwzwRo2eKLiQBnyEOUjhpN46vacV", 9999.99)]
		[InlineData(1, 1, "Gaming PC", "RTX ON", -1.0)]
		public void Give_Invalid_Input_ShouldReturnError(int id_brand, int id_category, string name, string description, double price) {
			ValidationResult dto_validation_result = new ProductPatchDTOValidator().Validate(new ProductPatchDTO { IDBrand = id_brand, IDCategory = id_category, Name = name, Description = description, Price = price });
			dto_validation_result.Errors.Count.Should().BeGreaterThan(0);
		}
		[Theory]
		[InlineData(0, 0, "", "", 0.0)]
		[InlineData(1, 1, "Gaming PC", "RTX ON", 9999.99)]
		public void Give_Valid_Input_ShouldNotReturnError(int id_brand, int id_category, string name, string description, double price) {
			ValidationResult dto_validation_result = new ProductPatchDTOValidator().Validate(new ProductPatchDTO { IDBrand = id_brand, IDCategory = id_category, Name = name, Description = description, Price = price });
			dto_validation_result.Errors.Count.Should().Be(0);
		}
	}
}