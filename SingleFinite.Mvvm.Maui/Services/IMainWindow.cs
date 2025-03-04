namespace SingleFinite.Mvvm.Maui.Services;

/// <summary>
/// Service used to get the main window for the app.
/// </summary>
public interface IMainWindow
{
    /// <summary>
    /// The current main window.
    /// </summary>
    Window? Current { get; }
}
