using FluentValidation;
namespace list_api.Models.Validators {
	public class ListProductValidator : AbstractValidator<ListProduct> {
		public ListProductValidator() { // Constructing.
			RuleFor(lp => lp.IDList).GreaterThan(0).WithMessage("List ID must be greater than 0.");
			RuleFor(lp => lp.IDProduct).GreaterThan(0).WithMessage("Product ID must be greater than 0.");
			RuleFor(lp => lp.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
		}
	}
}