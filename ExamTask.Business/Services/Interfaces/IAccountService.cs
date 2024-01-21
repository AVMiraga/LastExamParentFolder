using ExamTask.Business.ViewModels.AccountVms;
using ExamTask.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace ExamTask.Business.Services.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResult> CheckCredentialsAsync(LoginVm vm);
        Task<RegisterResult> RegisterAsync(RegisterVm vm);
        Task CreateRolesAsync();
    }

    public class LoginResult
    {
        public bool Success { get; set; }
        public AppUser? AppUser { get; set; }
    }

    public class RegisterResult
    {
        public IdentityResult IdentityResult { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
