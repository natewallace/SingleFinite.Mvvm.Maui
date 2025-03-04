using SingleFinite.Mvvm.Maui.Internal.Services;
using SingleFinite.Mvvm.Maui.Services;

/// <summary>
/// Implementation of <see cref="IMainWindow"/> and
/// <see cref="IMainWindowProvider"/>.
/// </summary>
internal class MainWindow : IMainWindow, IMainWindowProvider
{
    #region Properties

    /// <inheritdoc/>
    public Window? Current { get; set; }

    #endregion
}
