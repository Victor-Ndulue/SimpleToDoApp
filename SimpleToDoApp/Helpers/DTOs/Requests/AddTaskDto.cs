using SimpleToDoApp.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace SimpleToDoApp.Helpers.DTOs.Requests;

public record AddTaskDto
(
    string? title, string? description,
    TaskRepetitionInterval recurrence,
    DateTime? dueDateTime
);