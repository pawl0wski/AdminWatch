using AdminWatchServer.Models;
using Microsoft.AspNetCore.Identity;

namespace AdminWatchServer.Services;

public interface IAuthService
{
    public Task<IdentityResult> Register(string username, string password);

    public Task<bool> IsLoggedIn();

    public Task<LoginDeniedReason?> Login(string username, string password, bool isPersistent);

    public Task Logout();

    public bool IsNoAccounts();

    public Task DeleteUser(AdminWatchUser user);

    public Task ApproveUser(AdminWatchUser user);

    public Task UnApproveUser(AdminWatchUser user);

    public List<AdminWatchUser> GetAllUsers();

    public Task<bool> IsSuperAdmin(AdminWatchUser user);
}

public enum LoginDeniedReason
{
    BadPasswordOrUsername,
    NotApproved,
}
