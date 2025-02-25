﻿using FluentValidation;
using SimpleToDoApp.Helpers.DTOs.Requests;

namespace SimpleToDoApp.Helpers.DataValidations;

public class AccountCreationDtoValidation:AbstractValidator<AccountCreationDto>
{
    public AccountCreationDtoValidation()
    {
        RuleFor(dto=>dto.userName).RequiredField();
        RuleFor(dto => dto.userEmail).ValidOptionalEmailAddress();
        RuleFor(dto => dto.password).RequiredField()
            .ValidatePassword();
    }
}
