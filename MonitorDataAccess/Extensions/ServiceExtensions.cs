using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonitorDataAccess.Config;
using MonitorDataAccess.DataAccess;
using MonitorDataAccess.EF;

namespace MonitorDataAccess.Extensions;

public static class ServiceExtensions
{
    public static void AddDataAccessServices(this IServiceCollection services)
    {
        services.AddSingleton<FileMoodDataAccess>();
        services.AddSingleton<ImportFromMoodHistoryDataAccess>();
        services.AddSingleton<FileLogDataAccess>();
        services.AddScoped<DBLogDataAccess>();

        services.AddSingleton<ImportFromGoogleNotesDataAccess>();

        string dataDirectory = @"C:\Users\lchorley\source\repos\mood-monitor\DataManagement\Data\";
        string[] googleNotesFiles =
        [
            "GoogleNotes 21.10.31.txt",
            "GoogleNotes 22.10.31.txt",
            "GoogleNotes 23.07.26.txt",
            "GoogleNotes 23.11.05.txt",
            "GoogleNotes 24.03.17.txt",
        ];

        foreach (var file in googleNotesFiles)
        {
            services.AddKeyedSingleton<ITextDataSource, FileTextDataSource>($"GoogleNotesData", (sp, o) => new FileTextDataSource(Path.Combine(dataDirectory, file)));
        }

        string moodHistoryFilename = "Moodistory 20240129 132739.json";
        services.AddKeyedSingleton<ITextDataSource, FileTextDataSource>($"MoodHistoryData", (sp, o) => new FileTextDataSource(Path.Combine(dataDirectory, moodHistoryFilename)));
    }

    public static void ConfigureDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PubContext>(options =>
        {
            string? connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);
        });

        services.Configure<LocalFileData>(configuration.GetSection(LocalFileData.ConfigurationSection));

    }

    public static void EnsureDatabaseCreated(this IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var context = scope.ServiceProvider.GetService<PubContext>();
        context.Database.EnsureCreated();
    }
}
