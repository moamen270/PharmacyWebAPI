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
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISendGridEmail, SendGridEmail>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<CloudinarySetting>(config.GetSection("CloudinarySetting"));
            services.Configure<AuthMessageSenderOptions>(config.GetSection("SendGrid"));

            services.AddCors(p => p.AddPolicy("corsapp", builder =>
            {
                builder.WithOrigins("https://ezdrug.tech/api")
                       .AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            return services;
        }
    }
}