using FluentValidation;

namespace SimpleToDoApp.Helpers.DataValidations;

public static class SharedDtoValidation
{
    public static IRuleBuilderOptions<T, TProperty> 
        RequiredField<T, TProperty>
        (this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("This field cannot be empty.");
    }
    public static IRuleBuilder<T, string?> ValidOptionalEmailAddress<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return ruleBuilder.Matches(pattern).WithMessage("Please enter a valid email address");
    }
    public static IRuleBuilder<T, string?> ValidatePassword<T>
        (this IRuleBuilder<T, string?> ruleBuilder)
    {
        var validPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
        string errorMessage = "Password must contain at least 8 characters, one uppercase, one lowercase, one number and one special character";
        return ruleBuilder.Matches(validPattern).WithMessage(errorMessage);
    }
    public static IRuleBuilderOptions<T, DateTime?> 
        ValidOptionalDateTime<T>
        (this IRuleBuilder<T, DateTime?> ruleBuilder)
    {
        return ruleBuilder
        .Must(date => !date.HasValue || IsValidDateFormat(date.Value))
        .WithMessage("Please enter a valid date in the format yyyy-MM-ddTHH:mm:ss")
        .Must(date => !date.HasValue || date > DateTime.UtcNow.AddHours(1))
        .WithMessage("The date cannot be in the past or present");
    }
    private static bool IsValidDateFormat(DateTime date)
    {
        DateTime parsedDate;
        return DateTime.TryParseExact(date.ToString("yyyy-MM-ddTHH:mm:ss"),
            "yyyy-MM-ddTHH:mm:ss", null, System.Globalization.DateTimeStyles.None, out parsedDate);
    }
}
