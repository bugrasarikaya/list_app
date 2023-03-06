using FluentAssertions;
using list_api.Common;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.ValidatorTests {
	public class ParamValidatorTests : IClassFixture<RepositoryTestFixture> {
		[Theory]
		[InlineData(0)]
		public void Give_Invalid_IntInput_ShouldReturnError(int param_int) {
			ParamValidator param_valiadator = new ParamValidator(param_int);
			param_valiadator.Validate().Should().Be(false);
		}
		[Fact]
		public void Give_Invalid_DateTimeInput_ShouldReturnError() {
			DateTime param_date_time = new DateTime(2077, 11, 1);
			ParamValidator param_valiadator = new ParamValidator(param_date_time);
			param_valiadator.Validate().Should().Be(false);
		}
		[Theory]
		[InlineData("2077/11/1")]
		public void Give_Invalid_DateTimeStringInput_ToDateTimeParameter_ShouldReturnError(DateTime param_date_time) {
			ParamValidator param_valiadator = new ParamValidator(param_date_time);
			param_valiadator.Validate().Should().Be(false);
		}
		[Theory]
		[InlineData("2077/11/1")]
		public void Give_Invalid_DateTimeStringInput_ToStringParameter_ShouldReturnError(string param_date_time_string) {
			ParamValidator param_valiadator = new ParamValidator(param_date_time_string);
			param_valiadator.Validate().Should().Be(false);
		}
		[Theory]
		[InlineData("")]
		[InlineData("QpYDDXfO4A5jHYmeBYb3rbFBSddxSos2eeRzBBpo8nENlENXU74WOZsiOVd1vUYgCK7y1pHCR7VWM9BiO24yY0q6FAZNo2ku20oHH")]
		public void Give_Invalid_StringInput_ShouldReturnError(string param_name) {
			ParamValidator param_valiadator = new ParamValidator(param_name);
			param_valiadator.Validate().Should().Be(false);
		}
		[Theory]
		[InlineData(1)]
		public void Give_Valid_IDInput_ShouldNotReturnError(int param_int) {
			ParamValidator param_valiadator = new ParamValidator(param_int);
			param_valiadator.Validate().Should().Be(true);
		}
		[Fact]
		public void Give_Valid_DateTimeInput_ShouldNotReturnError() {
			DateTime param_date_time = new DateTime(2020, 11, 1);
			ParamValidator param_valiadator = new ParamValidator(param_date_time);
			param_valiadator.Validate().Should().Be(true);
		}
		[Theory]
		[InlineData("2020/11/1")]
		public void Give_Valid_DateTimeStringInput_ToDateTimeParameter_ShouldNotReturnError(DateTime param_date_time) {
			ParamValidator param_valiadator = new ParamValidator(param_date_time);
			param_valiadator.Validate().Should().Be(true);
		}
		[Theory]
		[InlineData("2020/11/1")]
		public void Give_Valid_DateTimeStringInput_ToStringParameter_ShouldNotReturnError(string param_date_time_string) {
			ParamValidator param_valiadator = new ParamValidator(param_date_time_string);
			param_valiadator.Validate().Should().Be(true);
		}
		[Theory]
		[InlineData("Dove")]
		public void Give_Valid_StringInput_ShouldNotReturnError(string param_name) {
			ParamValidator param_valiadator = new ParamValidator(param_name);
			param_valiadator.Validate().Should().Be(true);
		}
	}
}