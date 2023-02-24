﻿using FluentValidation;
using list_api.Models.ViewModels;
namespace list_api.Models.Validators {
	public class CategoryValidator : AbstractValidator<Category> {
		public CategoryValidator() { // Constructing.
			RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("Name cannot be empty.");
			RuleFor(c => c.Name).NotNull().MinimumLength(8).WithMessage("Name must have at least 8 characters.");
		}
	}
}