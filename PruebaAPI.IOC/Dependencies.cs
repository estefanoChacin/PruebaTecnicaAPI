using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PruebaAPI.BLL.Contract;
using PruebaAPI.BLL.Service;
using PruebaAPI.DAL.Contract;
using PruebaAPI.DAL.DBContext;
using PruebaAPI.DAL.Repository;
using PruebaAPI.UTILITY.Mapper;
using PruebaAPI.UTILITY.Security;
using System.Text;

namespace PruebaAPI.IOC
{
    public static class Dependencies
    {
        public static void InjectDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyDbContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("connection"), new MySqlServerVersion(new Version(8, 0, 25)));
            });
            var dbContext = services.BuildServiceProvider().GetRequiredService<MyDbContext>();
            dbContext.Database.Migrate();

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MapperProfile));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<JWTService>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
            {
                var jwtSettings = configuration.GetSection("JwtSettings");
                var secretKey = jwtSettings["SecretKey"];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ClockSkew = TimeSpan.Zero // No extra time for token expiration
                };
            });
        }
    }
}
