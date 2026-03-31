using Example.App.Views;
using System.Reflection;
using SingleFinite.Mvvm;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Example.Models.Services;
using Example.App.Services;

namespace Example.App;

public static class AppHostBuilderExtensions
{
    public static AppHostBuilder AddExampleViews(
        this AppHostBuilder builder
    ) => builder
        .AddServices(services =>
        {
            services
                .TryAddSingleton<IAppThemeManager, AppThemeManager>();
        })
        .AddViews(views => views.Scan(Assembly.GetAssembly(typeof(MainWindow))));
}
