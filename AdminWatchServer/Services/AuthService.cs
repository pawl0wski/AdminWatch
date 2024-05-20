using AdminWatchServer.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AdminWatchServer.Services;

public class AuthService(SignInManager<AdminWatchUser> signInManager, AuthenticationStateProvider authState) : IAuthService
{
    private const bool LockOnDefault = false;

    public async Task<IdentityResult> Register(string username, string password)
    {
        var newUser = new AdminWatchUser { UserName = username, LockoutEnabled = LockOnDefault };
        
        var result =  await signInManager.UserManager.CreateAsync(newUser, password);
        await signInManager.SignInAsync(newUser, false);

        await MakeFirstUserSuperAdmin(newUser);
        
        return result;
    }

    private async Task MakeFirstUserSuperAdmin(AdminWatchUser user)
    {
        if ((GetAllUsers().Count - 1) == 0)
        {
            await signInManager.UserManager.AddToRoleAsync(user, "SuperAdmin");
            await signInManager.UserManager.SetLockoutEnabledAsync(user, false);
        }
        else
        {
            await signInManager.UserManager.AddToRoleAsync(user, "Admin");
        }
    }

    public async Task<bool> IsLoggedIn()
    {
        var state = await authState.GetAuthenticationStateAsync();
        return state.User.Identity?.IsAuthenticated ?? false;
    }

    public async Task<bool> Login(string username, string password, bool isPersistent)
    {
        var state = await signInManager.PasswordSignInAsync(username, password, isPersistent, false);
        return !state.Succeeded;
    }

    public async Task Logout()
    {
        await signInManager.SignOutAsync();
    }

    public bool IsNoAccounts()
    {
        return !signInManager.UserManager.Users.Any();
    }

    public List<AdminWatchUser> GetAllUsers()
    {
        return signInManager.UserManager.Users.ToList();
    }

    public async Task<bool> IsSuperAdmin(AdminWatchUser user)
    {
        return await signInManager.UserManager.IsInRoleAsync(user, "SuperAdmin");
    }
}