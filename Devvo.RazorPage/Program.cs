using Devvo.Application.Interfaces;
using Devvo.Application.Services;
using Devvo.Domain.Entities;
using Devvo.Infra;
using Devvo.Infra.Data.Config;
using Devvo.Infra.Data.Interfaces;
using Devvo.Infra.Data.Repositories;
using Devvo.RazorPage.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("ConnectionDevvoChallenge");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        builder.Services.AddIdentity<Usuario, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

        builder.Services.AddScoped<IUsuarioAppService, UsuarioAppService>();
        builder.Services.AddScoped<IAutenticacaoAppService, AutenticacaoAppService>();
        builder.Services.AddScoped<IAnelAppService, AnelAppService>();
        builder.Services.AddScoped<IAnelRepository, AnelRepository>();

        AuthenticationSetup.AddAuthentication(builder.Services, builder.Configuration);


        // Add services to the container.
        builder.Services.AddRazorPages();

        var app = builder.Build();

        MigrationManager.MigrateDatabase(app.Services);

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }


        app.UseStatusCodePages(async context =>
        {
            if (context.HttpContext.Response.StatusCode == 401)
            {
                context.HttpContext.Response.Redirect("/Login");
            }
        });

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}