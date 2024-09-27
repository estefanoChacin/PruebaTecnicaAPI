using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruebaAPI.DAL.Contract;
using PruebaAPI.DAL.DBContext;
using PruebaAPI.DAL.Repository;

namespace PruebaAPI.IOC
{
    public static class Dependencies
    {
        public static void InjectDependency(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<MyDbContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("connection"), new MySqlServerVersion(new Version(8, 0, 25)));
            });
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        }
    }
}
