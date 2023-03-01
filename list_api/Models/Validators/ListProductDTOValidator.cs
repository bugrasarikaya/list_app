using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class ListProductDTOValidator : AbstractValidator<ListProductDTO> {
		public ListProductDTOValidator() { // Constructing.
			RuleFor(lp => lp.IDList).GreaterThan(0).WithMessage("List ID must be greater than 0.");
			RuleFor(lp => lp.IDProduct).GreaterThan(0).WithMessage("Product ID must be greater than 0.");
			RuleFor(lp => lp.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
		}
	}
}