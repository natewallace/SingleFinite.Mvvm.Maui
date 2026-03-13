using SingleFinite.Example.Models;
using SingleFinite.Mvvm;
using SingleFinite.Mvvm.Maui;

namespace Example.App.Views;

public partial class MainWindow : Window, IView<MainViewModel>
{
    public MainWindow(MainViewModel viewModel)
    {
        ViewModel = viewModel;

        Page = new ContentPage()
        {
            Content = new Grid
            {
                Children =
                {
                    new HostPresenter()
                    {
                        Source = ViewModel.Content
                    },
                    new DialogHostPresenter()
                    {
                        Source = ViewModel.Dialog
                    }
                }
            }
        };
    }

    public MainViewModel ViewModel { get; }
}
