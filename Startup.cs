using Etch.OrchardCore.ThemeSettings.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCore.Navigation;

namespace Etch.OrchardCore.ThemeSettings
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDataMigration, Migrations>();
            services.AddScoped<INavigationProvider, AdminMenu>();

            services.Configure<MvcOptions>((options) =>
            {
                options.Filters.Add(typeof(ThemeSettingsFilter));
            });
        }
    }
}
