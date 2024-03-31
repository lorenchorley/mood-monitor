using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoodModule.Entities;
using MoodModule.Services;

namespace MoodModule.Extensions;

public static class ServiceExtensions
{
    public static void AddMoodServices(this IServiceCollection services)
    {
        services.AddSingleton<MoodDataAccessService>();
    }

    public static void ConfigureMoodServices(this IServiceCollection services, IConfiguration configuration)
    {

    }
}
