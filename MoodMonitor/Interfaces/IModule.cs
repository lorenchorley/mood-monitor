using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace MoodMonitor.Interfaces;

public interface IModule
{
    RenderFragment GetListComponent();
    RenderFragment GetCreationComponent();
    RenderFragment GetNavLink();

    void AddServices(IServiceCollection services);
    void ConfigureDataAccess(IServiceCollection services, IConfiguration configuration);
}
