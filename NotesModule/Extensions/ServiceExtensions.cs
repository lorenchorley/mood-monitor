using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonitorDataAccess.DataAccess;
using NotesModule.Services;

namespace NotesModule.Extensions;

public static class ServiceExtensions
{
    public static void AddNotesServices(this IServiceCollection services)
    {
        services.AddSingleton<NotesDataAccessService>();
    }

    public static void ConfigureNotesServices(this IServiceCollection services, IConfiguration configuration)
    {
    }
}
