using FluentValidation;
namespace list_api.Models.Validators {
	public class StatusValidator : AbstractValidator<Status> {
		public StatusValidator() { // Constructing.
			RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("Name cannot be empty.");
		}
	}
}