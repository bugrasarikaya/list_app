using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class ClientUserDTOValidator : AbstractValidator<ClientUserDTO> {
		public ClientUserDTOValidator() { // Constructing.
			RuleFor(cud => cud.Name).NotNull().NotEmpty().WithMessage("Username cannot be empty.");
			RuleFor(cud => cud.Name).MinimumLength(8).WithMessage("Username must have at least 8 characters.");
			RuleFor(cud => cud.Name).MaximumLength(100).WithMessage("Username must be at most 100 characters.");
			RuleFor(cud => cud.Password).NotNull().NotEmpty().WithMessage("Password cannot be empty.");
			RuleFor(cud => cud.Password).MinimumLength(8).WithMessage("Password must have at least 8 characters.");
			RuleFor(cud => cud.Password).MaximumLength(100).WithMessage("Password must be at most 100 characters.");
		}
	}
}