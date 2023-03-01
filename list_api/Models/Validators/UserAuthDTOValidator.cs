using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class UserAuthDTOValidator : AbstractValidator<UserAuthDTO> {
		public UserAuthDTOValidator() { // Constructing.
			RuleFor(uad => uad.Name).NotNull().NotEmpty().WithMessage("Username cannot be empty.");
			RuleFor(uad => uad.Name).MinimumLength(8).WithMessage("Username must have at least 8 characters.");
			RuleFor(uad => uad.Name).MaximumLength(100).WithMessage("Username must be at most 100 characters.");
			RuleFor(uad => uad.Password).NotNull().NotEmpty().WithMessage("Password cannot be empty.");
			RuleFor(uad => uad.Password).MinimumLength(8).WithMessage("Password must have at least 8 characters.");
			RuleFor(uad => uad.Password).MaximumLength(100).WithMessage("Password must be at most 100 characters.");
		}
	}
}