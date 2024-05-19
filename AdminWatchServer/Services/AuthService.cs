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
        await signInManager.UserManager.SetLockoutEnabledAsync(newUser, false);
        await signInManager.SignInAsync(newUser, false);
        
        return result;
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
}