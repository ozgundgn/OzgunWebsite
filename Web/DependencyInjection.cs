using Application.Common.Interfaces;
using GioWebsite.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddScoped<IUser, CurrentUser>();

            services.AddHttpContextAccessor();

            //services.AddHealthChecks()
            //    .AddDbContextCheck<ApplicationDbContext>();

            services.AddExceptionHandler<CustomExceptionHandler>();

            services.AddRazorPages();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            services.AddEndpointsApiExplorer();

            //services.AddOpenApiDocument((configure, sp) =>
            //{
            //    configure.Title = "GioWebsite API";

            //});

            return services;
        }

        //public static IServiceCollection AddKeyVaultIfConfigured(this IServiceCollection services, ConfigurationManager configuration)
        //{
        //    var keyVaultUri = configuration["AZURE_KEY_VAULT_ENDPOINT"];
        //    if (!string.IsNullOrWhiteSpace(keyVaultUri))
        //    {
        //        configuration.AddAzureKeyVault(
        //            new Uri(keyVaultUri),
        //            new DefaultAzureCredential());
        //    }

        //    return services;
        //}
    }
}
