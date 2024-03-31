using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StatsModule.Extensions;

public static class ServiceExtensions
{
    public static void AddStatsServices(this IServiceCollection services)
    {

        //builder.Services.AddSingleton<ImportFromMoodHistoryDataAccess>();
        //builder.Services.AddSingleton<DBDataAccess>();

    }

    public static void ConfigureStatsServices(this IServiceCollection services, IConfiguration configuration)
    {
    }
}
