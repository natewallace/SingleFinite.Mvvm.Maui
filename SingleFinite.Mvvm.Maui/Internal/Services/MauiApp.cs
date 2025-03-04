using System.Diagnostics;
using SingleFinite.Mvvm.Maui.Services;
using SingleFinite.Mvvm.Services;

namespace SingleFinite.Mvvm.Maui.Internal.Services;

/// <summary>
/// Implementation of <see cref="IMauiApp"/>.
/// </summary>
/// <param name="appHost">Listen for closed event.</param>
/// <param name="mainWindow">Hold the main window for the app.</param>
/// <param name="viewBuilder">Used to build HostWindow view.</param>
/// <param name="exceptionHandler">Used to log unhandled exceptions.</param>
/// <param name="cancellationTokenProvider">
/// Used to unsubscribe observers.
/// </param>
/// <typeparam name="TMainViewModel">
/// The type of view model to build for the HostWindow.
/// </typeparam>
internal partial class MauiApp<TMainViewModel>(
    IAppHost appHost,
    IMainWindowProvider mainWindow,
    IViewBuilder viewBuilder,
    IExceptionHandler exceptionHandler,
    ICancellationTokenProvider cancellationTokenProvider
) :
    IMauiApp,
    IDisposable
    where TMainViewModel : IViewModel
{
    #region Fields

    /// <summary>
    /// Set to true when the app has been disposed.
    /// </summary>
    private bool _isDisposed;

    #endregion

    #region Methods

    /// <inheritdoc/>
    public void Start()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);

        if (mainWindow.Current is not null)
            return;

        var hostWindow = viewBuilder.Build<TMainViewModel>() as Window ??
            throw new InvalidOperationException(
                "The view for the host view model must be a Window."
            );

        mainWindow.Current = hostWindow;

        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            if (e.ExceptionObject is Exception ex)
                exceptionHandler.Handle(ex);
        };

        appHost.Closed
            .Observe()
            .OnEach(() => Application.Current?.CloseWindow(hostWindow))
            .On(cancellationTokenProvider.CancellationToken);
    }

    /// <inheritdoc/>
    public void Activate()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);

        if (mainWindow.Current is Window window)
            Application.Current?.ActivateWindow(window);
    }

    /// <summary>
    /// Mark this object as disposed.
    /// </summary>
    public void Dispose()
    {
        _isDisposed = true;
    }

    #endregion
}
