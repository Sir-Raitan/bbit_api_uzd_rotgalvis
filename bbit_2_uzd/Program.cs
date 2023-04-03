using bbit_2_uzd.Mapping;
using bbit_2_uzd.Models;
using bbit_2_uzd.Services;
using bbit_2_uzd.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace bbit_2_uzd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDatabaseConfig>();

            builder.Services.AddAutoMapper(typeof(ModelToResourceProfile), typeof(ResourceToModelProfile));
            //Set up security
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = builder.Configuration["Authentication:Authority"];
                    options.Audience = builder.Configuration["Authentication:Audience"];

                    options.TokenValidationParameters.ValidateAudience = true;
                    options.TokenValidationParameters.ValidateIssuer = true;
                    options.TokenValidationParameters.ValidateIssuerSigningKey = true;

                });
            builder.Services.AddAuthorization(options =>
                    {
                        options.AddPolicy("ApiScope", policy =>
                        {
                            policy.RequireAuthenticatedUser();
                            policy.RequireClaim("scope", "https://localhost:7299/api");
                        });
                        options.AddPolicy("RequireManagerPrivileges", policy => 
                        {
                            policy.RequireAuthenticatedUser();
                            policy.RequireRole("role", "Manager");
                        });
                    }
                );
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        ClientCredentials = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri(@$"{builder.Configuration["Authentication:Authority"]}/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {"https://localhost:7299/api", "API"}
                            }
                        }
                    }
                });//$"{builder.Configuration[]}/connect/token"),
                options.AddSecurityRequirement(new OpenApiSecurityRequirement 
                {
                    {   
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            }
                        },
                        new List<string>
                        {
                            "https://localhost:7299/api"
                        }
                    }
                });
            });

            builder.Services.AddScoped<IApartmentService, ApartmentService>();
            builder.Services.AddScoped<IHouseService, HouseService>();
            builder.Services.AddScoped<ITenantService, TenantService>();
            
            
            //webapp config
            builder.Services.AddCors(options => options.AddPolicy(
                name: "AllowWebAppConnection",
                policy => {
                    policy.WithOrigins("https://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                }
                ));
            var app = builder.Build();

            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowWebAppConnection");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}