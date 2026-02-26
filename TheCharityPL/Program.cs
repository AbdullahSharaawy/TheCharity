using Microsoft.Extensions.DependencyInjection;
using TheCharityBLL.Helpers;
using TheCharityBLL.Services.Abstraction;
using TheCharityBLL.Services.Repository;
using TheCharityDAL.Repositories.Abstraction;
using TheCharityDAL.Repositories.Implementation;
using TheCharityPL.Middlewares;

namespace TheCharityPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.TheCharityEnhancedConnectionString(builder.Configuration);
            builder.Services.TheCharityDependencyInjection();
            builder.Services.TheCharityIdentity(builder.Configuration);
            builder.Services.FoxArtEmailConfiguration(builder.Configuration);

            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                // This prevents unknown properties from crashing deserialization
            }); ;
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            builder.Services.AddScoped<IDonatedItemsRepository, DonatedItemsRepository>();

            builder.Services.AddScoped<IOrganizationService, OrganizationService>();
            builder.Services.AddScoped<IOrganizationQueryService,OrganizationQueryService>();
            builder.Services.AddScoped<IOrganizationContactService, OrganizationContactService>();
            builder.Services.AddScoped<IDonatedItemService, DonatedItemService>();
            builder.Services.AddScoped<IDonatedItemQueryService, DonatedItemQueryService>();
            builder.Services.AddScoped<IDonatedItemAnalyticsService, DonatedItemAnalyticsService>();
            builder.Services.AddScoped<IDonatedItemAttachmentService, DonatedItemAttachmentService>();   
            builder.Services.AddScoped<IDonatedItemImageService, DonatedItemImageService>();

            var app = builder.Build();

            app.MapHealthChecks("/health");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //global exception handling middleware
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}