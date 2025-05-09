using FluentValidation;
using EmployeeManagementApi.DTOs;

namespace EmployeeManagementApi.Validators
{
    public class EmployeeDataValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeDataValidator()
        {
            RuleFor(e => e.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MinimumLength(2).WithMessage("First name must be at least 2 characters.")
                .MaximumLength(50).WithMessage("First name should not exceed 50 characters.");

            RuleFor(e => e.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MinimumLength(2).WithMessage("Last name must be at least 2 characters.")
                .MaximumLength(50).WithMessage("Last name should not exceed 50 characters.");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email should not exceed 100 characters.");

            RuleFor(e => e.Position)
                .NotEmpty().WithMessage("Position is required.")
                .MaximumLength(100).WithMessage("Position should not exceed 100 characters.");
        }
    }
}