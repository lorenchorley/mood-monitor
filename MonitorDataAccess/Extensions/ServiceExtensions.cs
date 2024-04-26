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

        //builder.Services.AddSingleton<DBDataAccess>();

        services.AddSingleton<FileMoodDataAccess>();
        services.AddSingleton<ImportFromMoodHistoryDataAccess>();
        services.AddSingleton<FileLogDataAccess>();

        services.AddSingleton<ImportFromGoogleNotesDataAccess>();

        string googleNotesDirectory = @"C:\Users\lchorley\source\repos\mood-monitor\MonitorDataAccess\ExampleData\";
        string[] googleNotesFiles = new string[]
        {
            //"GoogleNotes 21.10.31.txt",
            //"GoogleNotes 22.10.31.txt",
            //"GoogleNotes 23.07.26.txt",
            //"GoogleNotes 23.11.05.txt",
            //"GoogleNotes 24.03.17.txt",
        };

        foreach (var file in googleNotesFiles)
        {
            services.AddKeyedSingleton<ITextDataSource, FileTextDataSource>($"GoogleNotesData", (sp, o) => new FileTextDataSource(Path.Combine(googleNotesDirectory, file)));
        }
        //services.AddKeyedSingleton<ITextDataSource, FileTextDataSource>("GoogleNotesExample", (sp, o) => new FileTextDataSource(@"C:\Users\lchorley\source\repos\mood-monitor\MonitorDataAccess\ExampleData\GoogleNotes.txt"));
    }

    public static void ConfigureDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<LocalFileData>(configuration.GetSection(LocalFileData.ConfigurationSection));


        using (PubContext context = new PubContext())
        {
            context.Database.EnsureCreated();
        }

    }
}
