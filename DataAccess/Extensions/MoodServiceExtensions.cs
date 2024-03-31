using DataAccess.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extensions;

public static class MoodServiceExtensions
{
    public static void AddMoodServices(this IServiceCollection services)
    {

        //builder.Services.AddSingleton<ImportFromMoodHistoryDataAccess>();
        //builder.Services.AddSingleton<DBDataAccess>();

        services.AddSingleton<FileDataAccess>();
    }

    public static void ConfigureMoodServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<LocalFileData>(configuration.GetSection(LocalFileData.ConfigurationSection));
    }
}
