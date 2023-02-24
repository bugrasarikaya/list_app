using FluentValidation;
namespace list_api.Models.Validators {
	public class RefreshTokenValidator : AbstractValidator<string> {
		public RefreshTokenValidator() { // Constructing.
			RuleFor(rt => rt).NotNull().NotEmpty().WithMessage("Refresh token cannot be empty.");
			RuleFor(rt => rt).MinimumLength(36).WithMessage("Refresh token must have at least 36 characters.");
			RuleFor(rt => rt).Matches(@"^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$").WithMessage("Refresh token is not valid.");
		}
	}
}