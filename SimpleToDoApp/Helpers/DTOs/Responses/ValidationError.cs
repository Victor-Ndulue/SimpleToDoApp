namespace SimpleToDoApp.Helpers.DTOs.Responses;

public class ValidationError
{
    public string? FieldName { get; set; }
    public string? ErrorMessage { get; set; }
}
