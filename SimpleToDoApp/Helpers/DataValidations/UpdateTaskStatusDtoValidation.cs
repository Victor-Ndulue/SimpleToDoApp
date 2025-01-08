using FluentValidation;
using SimpleToDoApp.Helpers.DTOs.Requests;

namespace SimpleToDoApp.Helpers.DataValidations;

public class UpdateTaskStatusDtoValidation:AbstractValidator<UpdateTaskStatusDto>
{
    public UpdateTaskStatusDtoValidation()
    {
        RuleFor(task => task.taskId).RequiredField();
        RuleFor(task => task.taskStatus).RequiredField();
    }
}
