using AdminWatchServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminWatchServer;

public class AdminWatchContext(DbContextOptions<AdminWatchContext> context) 
    : IdentityDbContext<AdminWatchUser>(context);