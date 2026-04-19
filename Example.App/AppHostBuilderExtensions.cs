using Example.App.Views;
using System.Reflection;
using SingleFinite.Mvvm;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Example.Models.Services;
using Example.App.Services;

namespace Example.App;

/// <summary>
/// Extensions for the <see cref="AppHostBuilder"/> class.
/// </summary>
public static class AppHostBuilderExtensions
{
    /// <summary>
    /// Register types defined in this assembly.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static AppHostBuilder AddExampleApp(
        this AppHostBuilder builder
    ) => builder
        .AddServices(services =>
        {
            services
                .TryAddSingleton<IAppThemeManager, AppThemeManager>();
        })
        .AddViews(views => views.Scan(Assembly.GetAssembly(typeof(MainWindow))));
}
