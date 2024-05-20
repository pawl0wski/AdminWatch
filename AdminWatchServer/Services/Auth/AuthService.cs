using AdminWatchServer.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AdminWatchServer.Services.Auth;

public class AuthService(SignInManager<AdminWatchUser> signInManager, AuthenticationStateProvider authState) : IAuthService
{
    private const bool LockOnDefault = false;

    public async Task<RegisterStatus> Register(string username, string password)
    {
        var newUser = new AdminWatchUser { UserName = username, LockoutEnabled = LockOnDefault };
        
        var result =  await signInManager.UserManager.CreateAsync(newUser, password);
        if (!result.Succeeded)
            return new RegisterStatus { Result = result };
        

        var wasSuperAdmin = await MakeFirstUserSuperAdmin(newUser);
        
        if (wasSuperAdmin)
            await signInManager.SignInAsync(newUser, false);
        
        return new RegisterStatus{RegisteredSuperUser = wasSuperAdmin, Result = result};
    }

    private async Task<bool> MakeFirstUserSuperAdmin(AdminWatchUser user )
    {
        if ((GetAllUsers().Count - 1) == 0)
        {
            await signInManager.UserManager.AddToRoleAsync(user, "SuperAdmin");
            user.ApprovedBySuperAdmin = true;
            await signInManager.UserManager.UpdateAsync(user);
            return true;
        }
        
        await signInManager.UserManager.AddToRoleAsync(user, "Admin");
        return false;
    }

    public async Task<bool> IsLoggedIn()
    {
        var state = await authState.GetAuthenticationStateAsync();
        return state.User.Identity?.IsAuthenticated ?? false;
    }

    public async Task<LoginDeniedReason?> Login(string username, string password, bool isPersistent)
    {
        var user = await signInManager.UserManager.FindByNameAsync(username);
        
        if (!user?.ApprovedBySuperAdmin ?? false)
            return LoginDeniedReason.NotApproved;
        
        var state = await signInManager.PasswordSignInAsync(username, password, isPersistent, false);
        if (!state.Succeeded)
            return LoginDeniedReason.BadPasswordOrUsername;

        return null;
    }

    public async Task Logout()
    {
        await signInManager.SignOutAsync();
    }

    public bool IsNoAccounts()
    {
        return !signInManager.UserManager.Users.Any();
    }

    public async Task DeleteUser(AdminWatchUser user)
    {
        await signInManager.UserManager.DeleteAsync(user);
    }

    public async Task ApproveUser(AdminWatchUser user)
    {
        user.ApprovedBySuperAdmin = true;
        await signInManager.UserManager.UpdateAsync(user);
    }

    public async Task UnApproveUser(AdminWatchUser user)
    {
        user.ApprovedBySuperAdmin = false;
        await signInManager.UserManager.UpdateAsync(user);
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