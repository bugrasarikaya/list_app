using FluentValidation;
using list_api.Models.DTOs;
using System.Reflection;
namespace list_api.Models.Validators {
	public class ListPatchDTOValidator : AbstractValidator<ListPatchDTO> {
		public ListPatchDTOValidator() { // Constructing.
			RuleFor(l => l.IDCategory).GreaterThan(0).WithMessage("Category ID must be greater than 0.");
			RuleFor(l => l.IDUser).GreaterThan(0).WithMessage("User ID must be greater than 0.");
			RuleFor(l => l.Name).MaximumLength(200).WithMessage("Name must be at most 200 characters.");
			RuleFor(l => l.Description).MaximumLength(200).WithMessage("Description must be at most 200 characters.");
		}
	}
}