using Microsoft.AspNetCore.Mvc;
using SimpleToDoApp.Helpers.DTOs.Requests;
using SimpleToDoApp.IServiceRepo;

namespace SimpleToDoApp.Controllers;

public class AuthController:BaseController
{
    private readonly IAuthServiceRepo _authServiceRepo;

    public AuthController(IAuthServiceRepo authServiceRepo)
    {
        _authServiceRepo = authServiceRepo;
    }

    [HttpPost("create-account")]
    public async Task<IActionResult>
        CreateAccountAsync
        ([FromBody]AccountCreationDto accountCreationDto)
    {
        var result = await _authServiceRepo.CreateAccountAsync (accountCreationDto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult>
        SignInAsync
        ([FromBody] SignInDto signInDto)
    {
        var result = await _authServiceRepo.SignInAsync(signInDto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("request-password-reset")]
    public async Task<IActionResult>
        ResetPasswordAsync
        ([FromForm] string userEmail)
    {
        var result = await _authServiceRepo.ResetPasswordAsync(userEmail);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult>
        ResetPasswordAsync
        ([FromBody] ResetPasswordWIthOtpParams resetPasswordDto)
    {
        var result = await _authServiceRepo.ResetPasswordWIthOtpAsync(resetPasswordDto);
        return StatusCode(result.StatusCode, result);
    }
}
