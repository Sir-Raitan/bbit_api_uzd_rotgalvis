using IdServer.Data;
using IdServer.Factories;
using IdServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.Extensions.DependencyInjection;
using Duende.IdentityServer.EntityFramework.Mappers;

namespace IdServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
            //identity db config
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString(name: "Identity");
                options.UseSqlite(connectionString, opt => opt.MigrationsAssembly(migrationsAssembly));
            });
            //identity config
            builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
                .AddClaimsPrincipalFactory<AppUserClaimsPrincipalFactory>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AppDbContext>();

            //add ientity server
            var IdServerConnectionString = builder.Configuration.GetConnectionString(name: "DefaultConnection");
            builder.Services.AddIdentityServer()
                .AddAspNetIdentity<ApplicationUser>()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlite(IdServerConnectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlite(IdServerConnectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            });

            //add server ui
            builder.Services.AddRazorPages();

            var app = builder.Build();

            app.UseHttpsRedirection();

            //allows fetching files from www root
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();
            app.MapRazorPages();

            //seed db
            if (app.Environment.IsDevelopment())
            {
                using var scope = app.Services.CreateScope();

                scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
                scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                //if (userManager.FindByNameAsync("janis.tests").Result == null)
                //{
                //    var i =userManager.CreateAsync(new ApplicationUser { 
                //        FirstName="Janis",
                //        LastName="Tests",
                //        UserName="janis.tests",
                //        Email="Janis.tests@fake.com"
                //    },"Passw0rd").Result;
                //}

                var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                //if (!context.ApiResources.Any())
                //{
                //    foreach (var resource in Config.ApiScopes)
                //    {
                //        context.ApiScopes.Add(resource.ToEntity());
                //    }
                //    context.SaveChanges();
                //}

                if (!context.ApiScopes.Any())
                {
                    foreach (var resource in Config.ApiScopes)
                    {
                        context.ApiScopes.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.Clients.Any())
                {
                    foreach (var client in Config.Clients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.IdentityResources)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }

            app.Run();
        }
    }
}