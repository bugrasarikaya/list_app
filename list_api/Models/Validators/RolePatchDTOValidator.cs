using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class RolePatchDTOValidator : AbstractValidator<RolePatchDTO> {
		public RolePatchDTOValidator() { // Constructing.
			RuleFor(rpd => rpd.Name).MaximumLength(100).WithMessage("Name must be at most 100 characters.");
		}
	}
}