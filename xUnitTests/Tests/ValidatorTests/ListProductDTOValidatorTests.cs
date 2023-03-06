using FluentAssertions;
using FluentValidation.Results;
using list_api.Models.DTOs;
using list_api.Models.Validators;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.ValidatorTests {
	public class ListProductDTOValidatorTests : IClassFixture<RepositoryTestFixture> {
		[Theory]
		[InlineData(0, 1, 1)]
		[InlineData(1, 0, 1)]
		[InlineData(1, 1, 0)]
		public void Give_Invalid_Input_ShouldReturnError(int id_list, int id_product, int quantity) {
			ValidationResult dto_validation_result = new ListProductDTOValidator().Validate(new ListProductDTO { IDList = id_list, IDProduct = id_product, Quantity = quantity });
			dto_validation_result.Errors.Count.Should().BeGreaterThan(0);
		}
		[Theory]
		[InlineData(1, 1, 1)]
		public void Give_Valid_Input_ShouldNotReturnError(int id_list, int id_product, int quantity) {
			ValidationResult dto_validation_result = new ListProductDTOValidator().Validate(new ListProductDTO { IDList = id_list, IDProduct = id_product, Quantity = quantity });
			dto_validation_result.Errors.Count.Should().Be(0);
		}
	}
}