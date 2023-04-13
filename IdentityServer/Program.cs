using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {

            
            var builder = WebApplication.CreateBuilder(args);

            //var connectionString = builder.Configuration.GetConnectionString(name: "DefaoultConnection");
            //var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

            //builder.Services.AddIdentityServer()
            //    .AddConfigurationStore(options => options.ConfigureDbContext = b =>  b.UseSqlite(connectionString, opt => opt.MigrationsAssembly(migrationsAssembly)))
            //    .AddOperationalStore(options => options.ConfigureDbContext = b => b.UseSqlite(connectionString, opt => opt.MigrationsAssembly(migrationsAssembly)));
            //.AddInMemoryClients(new List<Client>())
            //.AddInMemoryApiScopes(new List<ApiScope>());


            var app = builder
            .ConfigureServices()
            .ConfigurePipeline();

            app.UseIdentityServer();
            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}