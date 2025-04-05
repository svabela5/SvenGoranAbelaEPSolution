using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DataAccess.DataContext;
using DataAccess.Repositories;
using Domain.Models;

namespace Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<PollDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<PollDbContext>();
        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<PollDbContext>();
        builder.Services.AddScoped<PollUserRepository>();

        var repoSettings = 1;

        try
        {
            repoSettings = builder.Configuration.GetValue<int>("repoSettings"); // 1 means db; 2 means file, 3 means email, 4 means cloud
        }
        catch
        {
            repoSettings = 1;
        }



        switch (repoSettings)
        {
            case 1:
                builder.Services.AddScoped<IPollRepository, PollRepository>();
                break;
            case 2:
                builder.Services.AddScoped<IPollRepository, PollFileRepository>();
                break;
            default:
                builder.Services.AddScoped<IPollRepository, PollRepository>();
                break;
        }

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();
    }
}
