using SimpleToDoApp.Helpers.DTOs.Requests;
using SimpleToDoApp.Helpers.ObjectWrapper;

namespace SimpleToDoApp.IServiceRepo;

public interface IAuthServiceRepo
{
    Task<StandardResponse<string>>
        CreateAccountAsync
        (AccountCreationDto accountCreationDto);

    Task<StandardResponse<string>>
        SignInAsync
        (SignInDto signInDto);

    Task<StandardResponse<string>>
       ResetPasswordAsync
       (string userEmail);

    Task<StandardResponse<string>>
        ResetPasswordWIthOtpAsync
        (ResetPasswordWIthOtpParams resetPasswordWIthOtpParams);
}
