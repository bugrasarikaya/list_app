using FluentValidation;
namespace list_api.Models.Validators {
	public class IDValidator : AbstractValidator<int> {
		public IDValidator() { // Constructing.
			RuleFor(rt => rt).GreaterThan(0).WithMessage("ID must greater than 0.");
		}
	}
}