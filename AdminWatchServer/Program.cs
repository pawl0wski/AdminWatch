using AdminWatchServer.Components;
using AdminWatchServer.Models;
using AdminWatchServer.Services;
using AdminWatchServer.Services.Auth;
using AdminWatchServer.Services.Devices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;

namespace AdminWatchServer;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();


        ConfigureDatabase(ref builder);

        builder.Services.AddScoped<IDevicesRepository, DevicesRepository>();

        ConfigureAuthentication(ref builder);

       
        builder.Services.AddFluentUIComponents();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        
        app.UseAuthentication();
        app.UseAuthorization();

        // app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        await CreateRoles(app);
        
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }

    private static async Task CreateRoles(IHost app)
    {
        using var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService <RoleManager<IdentityRole>>();
        var roles = new[] { "SuperAdmin", "Admin"};
        foreach(var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    private static void ConfigureDatabase(ref WebApplicationBuilder builder)
    {
          
        var dbFolderPath = Path.Join(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AdminWatch");
        var dbFilePath = Path.Join(dbFolderPath, "database.db");
        
        builder.Services.AddDbContext<AdminWatchContext>(options =>
        {
            options.UseSqlite(new SqliteConnectionStringBuilder
            {
                DataSource = dbFilePath,
                Mode = SqliteOpenMode.ReadWriteCreate
            }.ToString());
        });
    }
    
    private static void ConfigureAuthentication(ref WebApplicationBuilder builder)
    {
        builder.Services
            .AddIdentity<AdminWatchUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }).AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AdminWatchContext>();
        

        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddAuthentication().AddCookie();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = new PathString("/login");
        });
    }
}