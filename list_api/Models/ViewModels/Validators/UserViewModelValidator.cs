using FluentValidation;
using list_api.Models.ViewModels;
namespace list_api.Models.Validators {
	public class UserViewModelValidator : AbstractValidator<UserViewModel> {
		public UserViewModelValidator() { // Constructing.
			RuleFor(uvm => uvm.Name).NotNull().NotEmpty().WithMessage("Username cannot be empty.");
			RuleFor(uvm => uvm.Name).NotNull().MinimumLength(8).WithMessage("Username must have at least 8 characters.");
			RuleFor(uvm => uvm.Password).NotNull().WithMessage("Password cannot be empty.");
			RuleFor(uvm => uvm.Password).NotNull().MinimumLength(8).WithMessage("Password must have at least 8 characters.");
		}
	}
}