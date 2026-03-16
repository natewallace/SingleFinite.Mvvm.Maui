using Example.Models;
using SingleFinite.Mvvm;
using SingleFinite.Mvvm.Maui;

namespace Example.App.Views;

/// <summary>
/// The main window for the app.
/// </summary>
public partial class MainWindow : Window, IView<MainViewModel>
{
    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="viewModel">The view model for this view.</param>
    public MainWindow(MainViewModel viewModel)
    {
        ViewModel = viewModel;

        Page = new MainWindowContentPage()
        {
            Content = new Grid
            {
                Children =
                {
                    new ViewPresenter()
                    {
                        Source = ViewModel.Content
                    },
                    new DialogViewPresenter()
                    {
                        Source = ViewModel.Dialog
                    }
                }
            }
        };
    }

    #endregion

    #region Properties

    /// <summary>
    /// The view model for this view.
    /// </summary>
    public MainViewModel ViewModel { get; }

    #endregion
}

/// <summary>
/// The content page for the main window.
/// </summary>
internal sealed partial class MainWindowContentPage : ContentPage
{
}
