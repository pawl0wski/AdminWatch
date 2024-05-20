using AdminWatchServer.Components;
using AdminWatchServer.Models;
using AdminWatchServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;

namespace AdminWatchServer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        
        var dbFolderPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AdminWatch");
        var dbFilePath = Path.Join(dbFolderPath, "database.db");
        
        builder.Services.AddDbContext<AdminWatchContext>(options =>
        {
            options.UseSqlite(new SqliteConnectionStringBuilder{DataSource = dbFilePath, Mode = SqliteOpenMode.ReadWriteCreate}.ToString());
        });

        builder.Services.AddCascadingAuthenticationState();
        

        builder.Services.AddScoped<IAuthService, AuthService>();
        
        builder.Services
            .AddIdentity<AdminWatchUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<AdminWatchContext>();
        
        builder.Services.AddAuthentication().AddCookie();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = new PathString("/login");
        });
        
        
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

        
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }

}