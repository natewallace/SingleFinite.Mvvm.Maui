using SingleFinite.Mvvm.Maui.Internal.Services;
using SingleFinite.Mvvm.Maui.Services;
using SingleFinite.Mvvm.Services;

namespace SingleFinite.Mvvm.Maui;

/// <summary>
/// Extensions for the <see cref="AppHostBuilder"/> class.
/// </summary>
public static class AppHostBuilderExtensions
{
    #region Methods

    /// <summary>
    /// Add Maui services to the app.
    /// </summary>
    /// <typeparam name="TMainViewModel">
    /// The type of view model that will be built as the entry point for app.
    /// The view registered for the view model must be of type 
    /// <see cref="Window"/>
    /// </typeparam>
    /// <param name="builder">The builder to extend.</param>
    /// <returns>The builder that was passed in.</returns>
    public static AppHostBuilder AddMaui<TMainViewModel>(
        this AppHostBuilder builder
    )
        where TMainViewModel : IViewModel => builder
            .AddServices(
                services =>
                {
                    services
                        .AddSingleton<MainWindow>()
                        .AddSingleton<IMainWindow>(serviceProvider =>
                        {
                            return serviceProvider.GetRequiredService<MainWindow>();
                        })
                        .AddSingleton<IMainWindowProvider>(serviceProvider =>
                        {
                            return serviceProvider.GetRequiredService<MainWindow>();
                        })
                        .AddSingleton<IAppDispatcherMain, DispatcherMainQueue>()
                        .AddSingleton<IMauiApp, MauiApp<TMainViewModel>>();
                }
            )
            .AddOnStarted<IMauiApp>(
                app => app.Start()
            );

    #endregion
}
