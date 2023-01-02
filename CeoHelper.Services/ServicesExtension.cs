using CeoHelper.Services.Interfaces;
using CeoHelper.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI_API;

namespace CeoHelper.Services;

public static class ServicesExtension
{
    public static IServiceCollection AddCeoServices(this IServiceCollection services)
    {
        services.AddScoped<ICeoService, CeoService>();
        services.AddSingleton((provider) =>
        {
            return new OpenAIAPI(provider.GetService<IConfiguration>().GetSection("OPENAI_KEY").Value, Engine.Davinci);
        });
        return services;
    }
}
