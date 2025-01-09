using FluentValidation;
using SimpleToDoApp.Helpers.DTOs.Requests;

namespace SimpleToDoApp.Helpers.DataValidations;

public class ResetPasswordWIthOtpParamsValidation:AbstractValidator<ResetPasswordWIthOtpParams>
{
    public ResetPasswordWIthOtpParamsValidation()
    {
        RuleFor(dto =>dto.userEmail).RequiredField()
            .ValidOptionalEmailAddress();
        RuleFor(dto => dto.otp).RequiredField();
        RuleFor(dto => dto.newPassword).RequiredField()
            .ValidatePassword();
    }
}
