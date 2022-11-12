using Microsoft.EntityFrameworkCore;
using UnitTestingMinimalApi.Data;
using UnitTestingMinimalApi.Repositories;

namespace UnitTestingMinimalApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void Register(this IServiceCollection services)
        {
            services.AddDbContext<PlayerContext>(options => options.UseInMemoryDatabase("Players"));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IPlayerContext, PlayerContext>();
        }
    }
}
