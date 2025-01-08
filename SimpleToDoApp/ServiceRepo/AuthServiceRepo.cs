using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleToDoApp.DataAccess.DataContext;
using SimpleToDoApp.Extensions.Configs;
using SimpleToDoApp.Helpers.DTOs.Requests;
using SimpleToDoApp.Helpers.ObjectWrapper;
using SimpleToDoApp.IServiceRepo;
using SimpleToDoApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleToDoApp.ServiceRepo;

public sealed class AuthServiceRepo : IAuthServiceRepo
{
    private readonly JwtSettings _jwtSettings;
    private readonly DbSet<AppUser> _appUsers;
    private readonly AppDbContext _appDbContext;
    private readonly IMailServiceRepo _mailService;

    public AuthServiceRepo(
        IOptions<JwtSettings> jwtSettings, AppDbContext appDbContext, 
        IMailServiceRepo mailService)
    {
        _jwtSettings = jwtSettings.Value;
        _appUsers = appDbContext.AppUsers;
        _appDbContext = appDbContext;
        _mailService = mailService;
    }

    public async Task<StandardResponse<string>>
        CreateAccountAsync
        (AccountCreationDto accountCreationDto)
    {
        string? validationErrorMsg = await ValidateAccountCreationDto(accountCreationDto);

        if (!string.IsNullOrWhiteSpace(validationErrorMsg))
        {
            return StandardResponse<string>.Failed(data: null, errorMessage: validationErrorMsg);
        }

        var appUser = new AppUser
        {
            Username = accountCreationDto.userName,
            Email = accountCreationDto.userEmail
        };
        appUser.CreatedBy = appUser.Id;

        var pwHasher = new PasswordHasher<string>();
        string hashedPassword = pwHasher.HashPassword(appUser.Username, accountCreationDto.password);
        appUser.PasswordHash = hashedPassword;

        await _appUsers.AddAsync(appUser);
        await _appDbContext.SaveChangesAsync();

        string successMsg = "Account created successfully. Proceed to sign in";
        return StandardResponse<string>.Success(data: successMsg);
    }

    public async Task<StandardResponse<string>>
        SignInAsync
        (SignInDto signInDto)
    {
        string errorMsg = "Invalid credentials";
        var user = await _appUsers.FirstOrDefaultAsync(user=> user.Username == signInDto.userName);
        if (user is null)
            return StandardResponse<string>.Failed(data: null, errorMessage: errorMsg);
        var pwHasher = new PasswordHasher<string>();

        var verifyPassword = pwHasher.VerifyHashedPassword(user.Username, user.PasswordHash, signInDto.password);

        if (verifyPassword == PasswordVerificationResult.Failed)
            return StandardResponse<string>.Failed(data: null, errorMessage: errorMsg);

        string token = CreateToken(user);

        return StandardResponse<string>.Success(data: token);
    }

    public async Task<StandardResponse<string>>
       ResetPasswordAsync
       (string userEmail)
    {
        var user = await _appUsers.FirstOrDefaultAsync(u=>u.Email == userEmail);
        string errorMsg = "Invalid User Credentials";
        if (user is not null)
        {
            var otp = ResetOtpTrackers(user);

            var sendOtpToUserResponse = await SendOTPMailAsync(user.Username, user.Email, otp);
            if(!sendOtpToUserResponse.Succeeded)
            {
                errorMsg = "Failed to send otp to user. Please try again later.";
                return StandardResponse<string>.Failed(data: null, errorMessage: errorMsg);
            }

            var updateUserResult = await _appDbContext.SaveChangesAsync();

            string successMsg = "Kindly check your mail for otp";
            return StandardResponse<string>.Success(data: successMsg);
        }
        return StandardResponse<string>.Failed(data: errorMsg);
    }

    public async Task<StandardResponse<string>>
        ResetPasswordWIthOtpAsync
        (ResetPasswordWIthOtpParams resetPasswordWIthOtpParams)
    {
        var user = await _appUsers.FirstOrDefaultAsync(u=> u.Email == resetPasswordWIthOtpParams.userEmail);
        string? errorMsg = string.Empty;
        if (user is null)
        {
            errorMsg = "Account does not exist";
            return StandardResponse<string>.Failed(data: null, errorMessage: errorMsg);
        }
        var confirmOtpResponse = ConfirmOtp(user, resetPasswordWIthOtpParams.otp);
        if (!confirmOtpResponse.Succeeded)
        {
            return confirmOtpResponse;
        }
        var pwHasher = new PasswordHasher<string>();
        string hashedPassword = pwHasher.HashPassword(user.Username, resetPasswordWIthOtpParams.newPassword);
        user.PasswordHash = hashedPassword;
        await _appDbContext.SaveChangesAsync();
        string successMsg = "Password reset successful. Kindly proceed to login";
        return StandardResponse<string>.Success(data: successMsg);
    }

    private async Task<IdentityResult>
        SendOTPMailAsync(string userName, string userEmail, string otp)
    {
        string senderEmail = "paybigie@gmail.com";
        string? mailSubject = "OTP Confirmation";
        string? mailBody = $"Dear {userName}, \n \n Kindly use the otp: {otp} to complete your request.\n The otp expires after 5 minutes.";
        bool isHtml = true;
        string? mailTemplateName = string.Empty;

        var sendEmailParams = new EmailParams
            (
                senderEmail: senderEmail,
                emailSubject: mailSubject,
                emailBody: mailBody,
                recipientEmail: userEmail,
                senderName: userName,
                isHtml: isHtml
            );

        var sendEmailResponse = await _mailService.SendEmailAsync(sendEmailParams);
        return sendEmailResponse;
    }

    //Resets and sets new otp details for a specified user. Ensure to persist chnages on calling method
    private string
        ResetOtpTrackers
        (AppUser user)
    {
        string authToken = GenerateOtp();
        user.OTP = authToken;
        user.OTPExpiryDate = DateTime.UtcNow.AddMinutes(5);
        user.OTPConfirmed = false;
        return authToken;
    }

    //Generates random digits for otp requests.
    private string
        GenerateOtp()
    {
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();

        byte[] randomNumber = new byte[5]; // Buffer to hold random bytes
        rng.GetBytes(randomNumber); // Fill buffer with secure random bytes

        // Convert the byte array to an integer
        int randomValue = Math.Abs(BitConverter.ToInt32(randomNumber, 0));

        // Get the first 4 digits of the random number
        string otp = (randomValue % 10000).ToString("D5");
        return otp;
    }

    //Used to confirm otp sent to a user. Ensure to persist changes on calling method. 
    private StandardResponse<string>
        ConfirmOtp
        (AppUser user, string otp)
    {
        if (otp != user.OTP)
        {
            var errorMsg = "Invalid token";
            return StandardResponse<string>.Failed(errorMsg);
        }
        if (!user.OTPConfirmed.HasValue)
        {
            user.OTPConfirmed = false;
        }
        if (user.OTPConfirmed.Value)
        {
            var errorMsg = "Request already validated";
            return StandardResponse<string>.Failed(errorMsg);
        }
        var currentDateTime = DateTime.UtcNow;
        if (currentDateTime > user.OTPExpiryDate)
        {
            var errorMsg = "OTP already expired. Regenerate";
            return StandardResponse<string>.Failed(errorMsg);
        }
        user.OTPConfirmed = true;

        var successMsg = "OTP Confirmed";
        return StandardResponse<string>.Success(successMsg);
    }

    private async Task<string>
        ValidateAccountCreationDto
        (AccountCreationDto accountCreationDto)
    {
        //The Password validation is placed ahead of asynchronous call to ensure it runs and not skipped
        string? errorMsg = accountCreationDto switch
        {
            { userEmail: var email } when await _appUsers.AnyAsync(user => user.Email == email) => "Email address already exists",
            { userName: var username } when await _appUsers.AnyAsync(user => user.Username == username) => "Username already taken",
            _ => string.Empty
        };
        return errorMsg;
    }

    private string
        CreateToken(AppUser user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.TokenKey));
        var claim = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claim),
            IssuedAt = DateTime.UtcNow,//Had to remove .AddHour(1) because it automatically add one hour for some reason
            Issuer = _jwtSettings.validIssuer,
            Audience = _jwtSettings.validAudience,
            Expires = DateTime.UtcNow.AddMinutes(Double.Parse(_jwtSettings.Expires)),
            SigningCredentials = credentials,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
