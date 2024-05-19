using Microsoft.AspNetCore.Identity;

namespace AdminWatchServer.Services;

public interface IAuthService
{
    public Task<IdentityResult> Register(string username, string password);

    public Task<bool> IsLoggedIn();

    public Task<bool> Login(string username, string password, bool isPersistent);

    public Task Logout();

    public bool IsNoAccounts();
}
