using FluentValidation;
using WhatsInt.Interface.Helpers;
using WhatsInt.Model;

namespace WhatsInt.Interface.Validator
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("User cannot be null");

            RuleFor(user => user)
                .NotEmpty().WithMessage("Email cannot be null")
                .Must(user => user.Email.ValidateEmail()).WithMessage("Invalid email");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password cannot be null");

            RuleFor(user => user)
                .Must(user => user.Password.ValidatePassword(user.PasswordConfirmation)).WithMessage("Invalid Password");

        }
    }
}
