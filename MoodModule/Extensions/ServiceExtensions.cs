using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonitorDataAccess.DataAccess;
using MoodModule.Entities;
using MoodModule.Services;

namespace MoodModule.Extensions;

public static class ServiceExtensions
{
    public static void AddMoodServices(this IServiceCollection services)
    {
        services.AddScoped<MoodDataAccessService>();
        services.AddScoped<DBStatsDataAccess>();
    }

    public static void ConfigureMoodServices(this IServiceCollection services, IConfiguration configuration)
    {

    }
}
