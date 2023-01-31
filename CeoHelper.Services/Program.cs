using CeoHelper.Data;
using CeoHelper.Services.Services.Interfaces;
using CeoHelper.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI_API;
using CeoHelper.Shared.Options;

namespace CeoHelper.Services
{
    public static class Program
    {
        public static void ConfigureBusinessLogic(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDataAccess(configuration);
            AddOpenAIAPI(services, configuration);
            AddDependecies(services);
        }

        private static void AddDependecies(IServiceCollection services)
        {
            services.AddScoped<ICeoService, CeoService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IUserService, UserService>();
        }

        private static void AddOpenAIAPI(IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.GetSection(nameof(AppSettings));
            var openAIAPIKey = appSettings.GetValue<string>(nameof(AppSettings.OpenAIKey));
            services.AddSingleton(x => new OpenAIAPI(openAIAPIKey, Engine.Davinci));
        }
    }
}
