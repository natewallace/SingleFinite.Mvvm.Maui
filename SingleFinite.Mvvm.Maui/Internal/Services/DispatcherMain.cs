using SingleFinite.Mvvm.Maui.Services;
using SingleFinite.Mvvm.Services;

namespace SingleFinite.Mvvm.Maui.Internal.Services;

/// <summary>
/// Implementation of IMainDispatcher that uses the
/// <see cref="Dispatcher"/> from the main window to execute functions.
/// </summary>
/// <param name="mainWindow">The main window for the app.</param>
/// <param name="cancellationTokenProvider">
/// The service that provides the CancellationToken used by this service.
/// </param>
/// <param name="exceptionHandler">
/// The exception handler used by this dispatcher to handle exceptions.
/// </param>
internal partial class DispatcherMainQueue(
    IMainWindow mainWindow,
    ICancellationTokenProvider cancellationTokenProvider,
    IExceptionHandler exceptionHandler
) :
    DispatcherBase(cancellationTokenProvider, exceptionHandler),
    IAppDispatcherMain,
    IDisposable
{
    #region Fields

    /// <summary>
    /// Set to true when this object has been disposed.
    /// </summary>
    private bool _isDisposed = false;

    #endregion

    #region Methods

    /// <summary>
    /// Queue execution of the function on the <see cref="Dispatcher"/> set
    /// for this dispatcher.
    /// </summary>
    /// <typeparam name="TResult">
    /// The type of result returned by the function.
    /// </typeparam>
    /// <param name="func">The function to execute.</param>
    /// <returns>A task that runs until the function has completed.</returns>
    public override Task<TResult> RunAsync<TResult>(Func<Task<TResult>> func)
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);

        var dispatcher = mainWindow.Current?.Dispatcher ??
            throw new InvalidOperationException("Main window is null.");

        return dispatcher.DispatchAsync(func);
    }

    /// <summary>
    /// Mark this object as disposed.
    /// </summary>
    public void Dispose()
    {
        if (_isDisposed)
            return;

        _isDisposed = true;
    }

    #endregion
}
