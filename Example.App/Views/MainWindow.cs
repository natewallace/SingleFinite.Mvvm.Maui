using CommunityToolkit.Maui.Markup;
using Example.Models;
using Fonts;
using SingleFinite.Essentials;
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

        Page = new NavigationPage
        (
            root: new ContentPage
            {
                ToolbarItems =
                {
                    new ToolbarItem
                    {
                        IconImageSource = new FontImageSource
                        {
                            FontFamily = FluentUI.FontFamily,
                            Glyph = FluentUI.settings_24_regular
                        },
                        Apply = it =>
                        {
                            it.Clicked += (_, _) => ViewModel.ShowSettings();
                        }
                    }
                },

                Content = new Grid
                {
                    new ViewPresenter
                    {
                        Source = ViewModel.Content
                    }
                }
            }
        ).DialogPage(
            new DialogViewPresenterPage
            {
                DialogViewPresenter = new DialogViewPresenter
                {
                    Source = ViewModel.Dialog
                }
            }
        );
    }

    #endregion

    #region Properties

    /// <summary>
    /// The view model for this view.
    /// </summary>
    public MainViewModel ViewModel { get; }

    #endregion
}
