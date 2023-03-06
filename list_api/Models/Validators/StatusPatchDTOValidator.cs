using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class StatusPatchDTOValidator : AbstractValidator<StatusPatchDTO> {
		public StatusPatchDTOValidator() { // Constructing.
			RuleFor(spd => spd.Name).MaximumLength(100).WithMessage("Name must be at most 100 characters.");
		}
	}
}