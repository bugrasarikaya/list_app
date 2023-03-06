using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class RoleDTOValidator : AbstractValidator<RoleDTO> {
		public RoleDTOValidator() { // Constructing.
			RuleFor(rd => rd.Name).NotNull().NotEmpty().WithMessage("Name cannot be empty.");
			RuleFor(rd => rd.Name).MaximumLength(100).WithMessage("Name must be at most 100 characters.");
		}
	}
}