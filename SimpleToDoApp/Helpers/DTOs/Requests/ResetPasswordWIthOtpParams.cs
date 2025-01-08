namespace SimpleToDoApp.Helpers.DTOs.Requests;

public record ResetPasswordWIthOtpParams
(string userEmail, string otp, string newPassword);
