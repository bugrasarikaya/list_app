using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class ClientListDTOValidator : AbstractValidator<ClientListDTO> {
		public ClientListDTOValidator() { // Constructing.
			RuleFor(cld => cld.IDCategory).GreaterThan(0).WithMessage("Category ID must be greater than 0.");
			RuleFor(cld => cld.Name).NotNull().NotEmpty().WithMessage("Name cannot be empty.");
			RuleFor(cld => cld.Name).MaximumLength(100).WithMessage("Name must be at most 100 characters.");
			RuleFor(cld => cld.Description).MaximumLength(200).WithMessage("Description must be at most 200 characters.");
		}
	}
}