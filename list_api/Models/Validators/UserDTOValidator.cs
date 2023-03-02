using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class UserDTOValidator : AbstractValidator<UserDTO> {
		public UserDTOValidator() { // Constructing.
			RuleFor(ud => ud.IDRole).GreaterThan(0).WithMessage("Role ID must be greater than 0.");
			RuleFor(ud => ud.Name).NotNull().NotEmpty().WithMessage("Username cannot be empty.");
			RuleFor(ud => ud.Name).MinimumLength(8).WithMessage("Username must have at least 8 characters.");
			RuleFor(ud => ud.Name).MaximumLength(100).WithMessage("Username must be at most 100 characters.");
			RuleFor(ud => ud.Password).NotNull().NotEmpty().WithMessage("Password cannot be empty.");
			RuleFor(ud => ud.Password).MinimumLength(8).WithMessage("Password must have at least 8 characters.");
			RuleFor(ud => ud.Password).MaximumLength(100).WithMessage("Password must be at most 100 characters.");
		}
	}
}