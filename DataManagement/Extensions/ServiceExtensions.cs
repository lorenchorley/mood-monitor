using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonitorDataAccess.Config;
using MonitorDataAccess.DataAccess;
using MonitorDataAccess.EF;

namespace DataManagement.Extensions;

public static class ServiceExtensions
{
    public static void AddDataAccessServices(this IServiceCollection services)
    {

    }

    public static void ConfigureDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {

    }
}
