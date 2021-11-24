
using GestionContactEF.Dal.DBContext;
using GestionContactEF.Dal.Models;
using GestionContactEF.Dal.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CVTheque.services.Extensions
{
    public static class ServicesExtension
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<Context>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("GestionContactEF"), b => b.MigrationsAssembly("GestionContact")));
        }
        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<Context>()
                .AddDefaultTokenProviders();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudience = "https://localhost:44309",
                    ValidIssuer = "https://localhost:44309/",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppSetting:Token").Value))
                };
            });
        }
        public static void ConfigureSeedUser(this IServiceCollection services)
        {
            services.AddTransient<SeedDb>();
        }
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            //services.AddTransient<IUserRepository, UserRepository>();
            //services.AddTransient<ILevelRepository, LevelRepository>();
        }
    }
}
