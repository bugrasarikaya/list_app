using FluentValidation;
namespace list_api.Models.Validators {
	public class StatusValidator : AbstractValidator<Status> {
		public StatusValidator() { // Constructing.
			RuleFor(s => s.Name).NotNull().NotEmpty().WithMessage("Name cannot be empty.");
		}
	}
}