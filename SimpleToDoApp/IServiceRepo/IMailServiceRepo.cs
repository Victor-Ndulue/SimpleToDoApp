using Microsoft.AspNetCore.Identity;
using SimpleToDoApp.Helpers.DTOs.Requests;

namespace SimpleToDoApp.IServiceRepo;

public interface IMailServiceRepo
{
    Task<IdentityResult> SendEmailAsync(EmailParams emailDTO);
}
