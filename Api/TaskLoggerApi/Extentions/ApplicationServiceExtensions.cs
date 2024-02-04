using Microsoft.EntityFrameworkCore;
using TaskLoggerApi.Data;
using TaskLoggerApi.Interfaces;
using TaskLoggerApi.Services;

namespace TaskLoggerApi.Extentions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services,IConfiguration config)
        {
            // Configure DbContext with the connection string
            services.AddDbContext<Data.ApiDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("connString")));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;

        }
    }
}
