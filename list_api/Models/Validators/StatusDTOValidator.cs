using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class StatusDTOValidator : AbstractValidator<StatusDTO> {
		public StatusDTOValidator() { // Constructing.
			RuleFor(sd => sd.Name).NotNull().NotEmpty().WithMessage("Name cannot be empty.");
			RuleFor(sd => sd.Name).MaximumLength(100).WithMessage("Name must be at most 100 characters.");
		}
	}
}