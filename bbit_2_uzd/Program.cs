using bbit_2_uzd.Mapping;
using bbit_2_uzd.Models;
using bbit_2_uzd.Services;
using bbit_2_uzd.Services.Interfaces;

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

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IApartmentService, ApartmentService>();
            builder.Services.AddScoped<IHouseService, HouseService>();
            builder.Services.AddScoped<ITenantService, TenantService>();
            
            //webapp config
            builder.Services.AddCors(options => options.AddPolicy(
                name: "AllowWebAppConnection",
                policy =>{
                    policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
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

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}