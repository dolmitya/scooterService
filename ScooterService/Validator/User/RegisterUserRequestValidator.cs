using FluentValidation;
using ScooterService.Controllers.Users.Entities;

namespace ScooterService.Validator.User;

public class RegisterUserRequestValidator: AbstractValidator<RegisterUserRequest>

{

    public RegisterUserRequestValidator()

    {

        RuleFor(x => x.Login)

            .NotEmpty()

            .Matches(@"[\w_]+")

            .WithMessage("Login is required");

        RuleFor(x => x.PasswordHash)

            .NotEmpty()

            .WithMessage("Password is required");

        RuleFor(x => x.Mail)

            .NotEmpty()

            .EmailAddress()

            .WithMessage("Email is required");

        RuleFor(x => x.Name)

            .NotEmpty()

            .MinimumLength(2)

            .MaximumLength(200)

            .WithMessage("Name is required");

        RuleFor(x => x.Surname)

            .NotEmpty()

            .MinimumLength(2)

            .MaximumLength(200)

            .WithMessage("Surname is required");

        RuleFor(x => x.Patronymic)

            .MinimumLength(0)

            .MaximumLength(200)

            .WithMessage("Patronymic is required");

        RuleFor(x => x.DateOfBirth)

            .Must(y => y == null || y < DateTime.UtcNow.AddYears(-18))

            .WithMessage("Birth date is required");

        RuleFor(x => x.RoleId)

            .NotEmpty()

            .WithMessage("Permission is required");

    }

}