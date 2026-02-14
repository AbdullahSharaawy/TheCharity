using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheCharityDAL.Database;
using TheCharityDAL.Entities;
using TheCharityDAL.Repositories.Abstraction;
using TheCharityDAL.Repositories.Implementation;

namespace TheCharityBLL.Helpers
{
    public static class ServiceExtensions
    {
        public static void TheCharityIdentity(this IServiceCollection services)
        {
            services.AddDataProtection();
            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<TheCharityDbContext>()
    .AddDefaultTokenProviders();
        }
        public static void TheCharityEnhancedConnectionString(this IServiceCollection services, IConfiguration configuration, string stringName = "defaultConnection")
        {
            var connectionString = configuration.GetConnectionString(stringName);
            services.AddDbContext<TheCharityDbContext>(options =>
                options.UseSqlServer(connectionString));
        }
        public static void TheCharityDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<IDonatedItemsRepository, DonatedItemsRepository>();
            services.AddScoped<IDonationRepository, DonationRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
        public static void ThirdPartyAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    // Configure Google authentication options here
                    options.ClientId = "";
                    options.ClientSecret = "";
                })
                .AddFacebook(options =>
                {
                    // Configure FaceBook authentication options here
                    options.AppId = "";
                    options.AppSecret = "";
                });
        }
    }
}
