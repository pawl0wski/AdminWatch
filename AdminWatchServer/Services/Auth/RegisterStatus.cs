using Microsoft.AspNetCore.Identity;

namespace AdminWatchServer.Services.Auth;

public record RegisterStatus
{
    public bool? RegisteredSuperUser { get; set; }

    public required IdentityResult Result { get; set; }

    public IEnumerable<IdentityError> GetErrors() => Result.Errors;

    public bool Succeeded() => Result.Succeeded;
    public bool HaveAnyErrors() => Result.Errors.Any();
}