namespace SingleFinite.Mvvm.Maui.Internal.Services;

/// <summary>
/// Service used to get and set the main window for the app.
/// </summary>
internal interface IMainWindowProvider
{
    /// <summary>
    /// The current main window.
    /// </summary>
    Window? Current { get; set; }
}
