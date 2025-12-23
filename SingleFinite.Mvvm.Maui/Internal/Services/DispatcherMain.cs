using SingleFinite.Essentials;
using SingleFinite.Mvvm.Maui.Services;
using SingleFinite.Mvvm.Services;

namespace SingleFinite.Mvvm.Maui.Internal.Services;

/// <summary>
/// Implementation of IMainDispatcher that uses the
/// <see cref="Dispatcher"/> from the main window to execute functions.
/// </summary>
internal partial class DispatcherMain :
    IApplicationMainDispatcher,
    IMainDispatcher,
    IDisposable
{
    #region Fields

    /// <summary>
    /// Holds the dispose state for this object.
    /// </summary>
    private readonly DisposeState _disposeState;

    /// <summary>
    /// Holds the main window.
    /// </summary>
    private readonly IMainWindow _mainWindow;

    /// <summary>
    /// Holds the exception handler.
    /// </summary>
    private readonly IExceptionHandler _exceptionHandler;

    #endregion

    #region Properties

    /// <inheritdoc/>
    public CancellationToken CancellationToken => throw new NotImplementedException();

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mainWindow">The main window for the app.</param>
    /// <param name="exceptionHandler">
    /// The exception handler used by this dispatcher to handle exceptions.
    /// </param>
    public DispatcherMain(
        IMainWindow mainWindow,
        IExceptionHandler exceptionHandler
    )
    {
        _mainWindow = mainWindow;
        _exceptionHandler = exceptionHandler;

        _disposeState = new DisposeState(owner: this);
    }

    #endregion

    #region Methods

    /// <inheritdoc/>
    public Task<TResult> RunAsync<TResult>(
        Func<Task<TResult>> func,
        CancellationToken cancellationToken = default
    )
    {
        _disposeState.ThrowIfDisposed();

        var dispatcher = _mainWindow.Current?.Dispatcher ??
            throw new InvalidOperationException("Main window is null.");

        var taskCompletionSource = new TaskCompletionSource<TResult>();

        dispatcher.DispatchAsync(async () =>
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                taskCompletionSource.SetResult(await func());
            }
            catch (Exception ex)
            {
                taskCompletionSource.SetException(ex);
            }
        });

        return taskCompletionSource.Task;
    }

    /// <summary>
    /// Use ExceptionHandler to handle exception.
    /// </summary>
    /// <param name="ex">The exception to handle.</param>
    public void OnError(Exception ex) => _exceptionHandler.Handle(ex);

    /// <inheritdoc/>
    public void Dispose() => _disposeState.Dispose();

    #endregion
}
