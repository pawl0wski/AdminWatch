using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace AdminWatchServer.Models;

public class AdminWatchUser : IdentityUser
{
    [DefaultValue(false)]
    public bool ApprovedBySuperAdmin { get; set; }
}