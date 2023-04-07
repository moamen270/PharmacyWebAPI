using Microsoft.OpenApi.Models;
using PharmacyWebAPI.DataAccess.Repository;
using PharmacyWebAPI.Utility.Services;
using PharmacyWebAPI.Utility.Services.IServices;
using PharmacyWebAPI.Utility.Settings;

namespace PharmacyWebAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JWT>(config.GetSection("JWT"));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISendGridEmail, SendGridEmail>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<AuthMessageSenderOptions>(config.GetSection("SendGrid"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EzDrugs API", Version = "v 3.1" });
            });

            services.AddCors(p => p.AddPolicy("corsapp", builder =>
            {
                builder.WithOrigins("https://api.ezdrug.tech")
                       .AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            return services;
        }
    }
}