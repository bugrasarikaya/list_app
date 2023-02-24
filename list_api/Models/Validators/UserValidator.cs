using FluentValidation;
namespace list_api.Models.Validators {
	public class UserValidator : AbstractValidator<User> {
		public UserValidator() { // Constructing.
			RuleFor(u => u.Name).NotNull().NotEmpty().WithMessage("Username cannot be empty.");
			RuleFor(u => u.Name).NotNull().MinimumLength(8).WithMessage("Username must have at least 8 characters.");
			RuleFor(u => u.Password).NotNull().WithMessage("Password cannot be empty.");
			RuleFor(u => u.Password).NotNull().MinimumLength(8).WithMessage("Password must have at least 8 characters.");
		}
	}
}