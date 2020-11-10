using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using System;
using System.Threading.Tasks;

namespace Etch.OrchardCore.ThemeSettings
{
    public class AdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer S;

        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            S = localizer;
        }

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            builder
                .Add(S["Design"], NavigationConstants.AdminMenuDesignPosition, design => design
                    .AddClass("themes").Id("themes")
                    .Add(S["Theme Settings"], "ThemeSettings", installed => installed
                        .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = "ThemeSettings" })
                        .LocalNav()
                    )
                );

            return Task.CompletedTask;
        }
    }
}
