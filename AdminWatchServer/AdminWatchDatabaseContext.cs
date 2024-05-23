using AdminWatchServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminWatchServer;

public class AdminWatchContext(DbContextOptions<AdminWatchContext> context)
    : IdentityDbContext<AdminWatchUser>(context)
{
    public DbSet<Device> Devices { get; set; }
    
    public DbSet<DeviceInfo> DeviceInfos { get; set; }

    public DbSet<DeviceCpuUtilization> DeviceCpuUtilizations { get; set; }
}