using ExamTask.Business.Enums;
using ExamTask.Business.Services.Interfaces;
using ExamTask.Business.ViewModels.AccountVms;
using ExamTask.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace ExamTask.Business.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<LoginResult> CheckCredentialsAsync(LoginVm vm)
        {
            var user = await _userManager.FindByEmailAsync(vm.LoginID) ?? await _userManager.FindByNameAsync(vm.LoginID);

            if(user is null)
            {
                return new LoginResult()
                {
                    Success = false,
                    AppUser = null
                };
            }

            var res = await _userManager.CheckPasswordAsync(user, vm.Password);

            return new LoginResult()
            {
                AppUser = user,
                Success = res
            };
        }

        public async Task CreateRolesAsync()
        {
            foreach (var item in Enum.GetValues(typeof(MyRoles)))
            {
                if(!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    IdentityRole role = new IdentityRole()
                    {
                        Name = item.ToString()
                    };

                    await _roleManager.CreateAsync(role);
                }
            }
        }

        public async Task<RegisterResult> RegisterAsync(RegisterVm vm)
        {
            AppUser user = new AppUser()
            {
                UserName = vm.Username,
                Email = vm.Email,
                FirstName = vm.FirstName,
                LastName = vm.LastName
            };

            var res = await _userManager.CreateAsync(user, vm.Password);

            if(!res.Succeeded)
            {
                return new RegisterResult()
                {
                    IdentityResult = res,
                    AppUser = null
                };
            }

            string RoleToAdd = MyRoles.Moderator.ToString(); //Change if needed => Admin, Moderator, User

            if(!await _roleManager.RoleExistsAsync(RoleToAdd))
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = RoleToAdd
                };

                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user, RoleToAdd);

            return new RegisterResult()
            {
                IdentityResult = res,
                AppUser = user
            };
        }
    }
}
