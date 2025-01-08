using FluentValidation;
using SimpleToDoApp.Helpers.DTOs.Requests;

namespace SimpleToDoApp.Helpers.DataValidations;

public class UpdateTaskDtoValidation : AbstractValidator<UpdateTaskDto>
{
    public UpdateTaskDtoValidation()
    {
        RuleFor(task => task.taskId).RequiredField();
        RuleFor(task => task.dueDateTime).ValidOptionalDateTime();
    }
}
