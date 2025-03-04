namespace SingleFinite.Mvvm.Maui.Services;

/// <summary>
/// The root object for the application responsible for initializing the
/// host window and launching it.
/// </summary>
public interface IMauiApp
{
    /// <summary>
    /// Start the app.
    /// </summary>
    void Start();

    /// <summary>
    /// Bring the app to the foreground.
    /// </summary>
    void Activate();
}
