using FluentValidation;
using SimpleToDoApp.Helpers.DTOs.Requests;

namespace SimpleToDoApp.Helpers.DataValidations;

public class AddTaskDtoValidation : AbstractValidator<AddTaskDto>
{
    public AddTaskDtoValidation()
    {
        RuleFor(dto=>dto.title).RequiredField();
        RuleFor(dto=>dto.dueDateTime).ValidOptionalDateTime();
    }
}
