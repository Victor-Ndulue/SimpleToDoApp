using FluentValidation;
using SimpleToDoApp.Helpers.DTOs.Requests;

namespace SimpleToDoApp.Helpers.DataValidations;

public class SignInDtoValidation:AbstractValidator<SignInDto>
{
    public SignInDtoValidation()
    {
        RuleFor(dto=> dto.userName).RequiredField();
        RuleFor(dto => dto.password).RequiredField();
    }
}
