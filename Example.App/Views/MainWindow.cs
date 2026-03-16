using Example.Models;
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

        Page = new ContentPage().Also(page =>
            page.Content = new Grid().Also(grid =>
            {
                grid.Children.Add(
                    new ViewPresenter().Also(content =>
                    {
                        content.Source = ViewModel.Content;
                        content.EnterForwardAnimation = IViewAnimation.SlideIn(
                            direction: SlideDirection.EndToStart
                        );
                        content.EnterBackwardAnimation = IViewAnimation.SlideIn(
                            direction: SlideDirection.StartToEnd
                        );
                        content.ExitForwardAnimation = IViewAnimation.None();
                        content.ExitBackwardAnimation = IViewAnimation.None();
                    })
                );

                grid.Children.Add(
                    new DialogViewPresenter().Also(dialog =>
                    {
                        dialog.Source = ViewModel.Dialog;
                    })
                );
            })
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
