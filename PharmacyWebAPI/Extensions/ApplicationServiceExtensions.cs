using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PharmacyWebAPI.Utility.Services.IServices;
using PharmacyWebAPI.Utility.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyWebAPI.DataAccess.Repository.IRepository;
using PharmacyWebAPI.DataAccess.Repository;
using PharmacyWebAPI.Utility;

namespace PharmacyWebAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
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
                builder.WithOrigins("http://ezdrug.somee.com/api").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
            }));

            return services;
        }
    }
}